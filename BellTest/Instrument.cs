using BellTest.Codes;
using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace BellTest
{
    /// <summary>
    /// The main window of the application, representing the instrument and displaying instructions/feedback to the user.
    /// </summary>
    public partial class Instrument : Form
    {
        public CodeList[] AllCodes;

        private delegate void InstrumentUiCallback();

        private delegate void InstrumentUiBoolCallback(bool arg);

        private delegate void InstrumentUiInstrumentModeCallback(InstrumentMode arg);

        private delegate CodeList InstrumentUiSelectedListCallback();

        public List<TapEvent> RecentTapEvents;

        private readonly SoundPlayer bellPlayer;

        public BellCode CurrentCode;
        public BellCode ExpectedCode;
        public BellCode ChosenCode;

        private Random _random;

        private Thread InstrumentWatcherThread;
        private Thread InstrumentPlayerThread;

        private int doneCount = 1;
        private int correctCount = 0;

        public Instrument()
        {
            InitializeComponent();
            comboModeSelect.Items.Add(new InstrumentModeValue(InstrumentMode.Send));
            comboModeSelect.Items.Add(new InstrumentModeValue(InstrumentMode.Receive));
            AllCodes = CodeList.Load(Properties.Settings.Default.CodeListFileName);
            foreach (CodeList list in AllCodes)
            {
                comboListSelect.Items.Add(list);
            }
            comboListSelect.SelectedIndex = 0;

            bellPlayer = new SoundPlayer(Properties.Settings.Default.BellSoundFileName);
            RecentTapEvents = new List<TapEvent>();
            _random = new Random();
            comboModeSelect.SelectedIndex = 0;
        }

        // Called to set up sending mode: kill the player thread, start the watcher thread, pick a code and display a belling request.
        private void StartSendMode()
        {
            if (InstrumentPlayerThread != null)
            {
                InstrumentPlayerThread.Abort();
                InstrumentPlayerThread = null;
            }
            if (InstrumentWatcherThread != null)
            {
                InstrumentWatcherThread.Abort();
            }
            InstrumentWatcherThread = new Thread(StatusCheckRunner);
            InstrumentWatcherThread.Start();
            comboCodeList.Visible = false;
            ExpectedCode = SelectRandomCode();
            lblInstruction.Text = "Please bell:\n" + ExpectedCode.Name;
            lblInstruction.Visible = true;
            RecentTapEvents.Clear();
            needle.ActiveState = false;
            CurrentCode = null;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            PlungerPushed();
        }

        // Called when the plunger is down: deflect the needle, record the event, if the other signalman is expecting to withdraw or switch out then ask the needle to queue the appropriate action.
        private void PlungerPushed()
        {
            needle.ActiveState = true;
            RecentTapEvents.Add(new TapEvent(TapEventType.Push));
            if (ExpectedCode.FinalBellPending(CurrentCode))
            {
                if (ExpectedCode.IsTokenRelease)
                {
                    needle.TriggerTokenRelease();
                }
                else if (ExpectedCode.IsSwitchingRelease)
                {
                    needle.TriggerSwitchOut();
                }
            }
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            PlungerReleased();
        }

        // Called when the plunger is released: restore the needle, record the event, and regenerate the entered code.
        private void PlungerReleased()
        {
            needle.LocalPlungerReleased();
            needle.ActiveState = false;
            RecentTapEvents.Add(new TapEvent(TapEventType.Release));
            BuildCode();
        }

        // Convert a list of tap events to a bell signal.
        private void BuildCode()
        {
            if (RecentTapEvents.Count < 2)
            {
                return;
            }
            if (CurrentCode == null)
            {
                CurrentCode = new BellCode();
            }
            if (RecentTapEvents.Count == 2 ||
                (RecentTapEvents[RecentTapEvents.Count - 2].Timestamp - RecentTapEvents[RecentTapEvents.Count - 3].Timestamp).TotalSeconds > Properties.Settings.Default.GroupPauseThreshold)
            {
                CurrentCode.BellGroups.Add(new BellGroup());
            }
            CurrentCode.BellGroups[CurrentCode.BellGroups.Count - 1].Bells
                .Add((RecentTapEvents[RecentTapEvents.Count - 1].Timestamp - RecentTapEvents[RecentTapEvents.Count - 2].Timestamp).TotalSeconds > Properties.Settings.Default.HoldDownThreshold 
                ? BellStroke.Hold
                : BellStroke.Normal);
        }

        /// <summary>
        /// The method behind the InstrumentWatcherThread.  Wakes every half-second to check if the plunger has not been touched for longer than the code-end threshold, and if so, work out if the 
        /// signal sent is the signal requested to be sent then display a new belling request.
        /// </summary>
        public void StatusCheckRunner()
        {
            while (true)
            {
                BlockForCodeEnd();

                if (CurrentCode == ExpectedCode)
                {
                    correctCount++;
                    CorrectUpdateScore();
                }
                else
                {
                    WrongUpdateScore(InstrumentMode.Send);
                }

                Thread.Sleep(2000);
                doneCount++;
                CurrentCode = null;
                ExpectedCode = SelectRandomCode();
                RecentTapEvents.Clear();
                ResetResultLabel();
            }
        }

        private void BlockForCodeEnd()
        {
            while (true)
            {
                Thread.Sleep((int)(1000 * Properties.Settings.Default.CodeEndPollingPeriod));
                if (RecentTapEvents.Count == 0)
                {
                    continue;
                }
                if (RecentTapEvents[RecentTapEvents.Count - 1].EventType == TapEventType.Push)
                {
                    continue;
                }
                if ((DateTime.Now - RecentTapEvents[RecentTapEvents.Count - 1].Timestamp).TotalSeconds < Properties.Settings.Default.CodeEndedThreshold)
                {
                    continue;
                }
                break;
            }
        }

        // Display a belling request safely.
        private void ResetResultLabel()
        {
            if (InvokeRequired)
            {
                Invoke(new InstrumentUiCallback(ResetResultLabel));
                return;
            }
            lblResult.Visible = false;
            lblInstruction.Text = "Please bell:\n" + ExpectedCode.Name;
        }

        // Display "Correct!" and the score, safely.
        private void CorrectUpdateScore()
        {
            if (InvokeRequired)
            {
                Invoke(new InstrumentUiCallback(CorrectUpdateScore));
                return;
            }
            lblResult.Text = "Correct!";
            UpdateScore();
            lblResult.Visible = true;
        }

        // Display the score.
        private void UpdateScore()
        {
            lblScore.Text = "Score: " + correctCount + "/" + doneCount;
        }

        // Display the user's mistake, safely.
        private void WrongUpdateScore(InstrumentMode mode)
        {
            if (InvokeRequired)
            {
                Invoke(new InstrumentUiInstrumentModeCallback(WrongUpdateScore), mode);
                return;
            }
            if (mode == InstrumentMode.Send)
            {
                lblResult.Text = "Wrong!  You belled " + CurrentCode + ", not " + ExpectedCode;
            }
            else
            {
                lblResult.Text = "Wrong!  You heard " + ChosenCode + ", not " + ((HiddenCode)comboCodeList.SelectedItem).Code;
            }
            UpdateScore();
            lblResult.Visible = true;
        }

        private BellCode SelectRandomCode()
        {
            CodeList selectedList = GetSelectedCodeList();
            return selectedList.Codes[_random.Next(selectedList.Codes.Count)];
        }

        private CodeList GetSelectedCodeList()
        {
            if (InvokeRequired)
            {
                return (CodeList)Invoke(new InstrumentUiSelectedListCallback(GetSelectedCodeList));
            }
            return (CodeList) comboListSelect.SelectedItem;
        }

        // Play a code.  Should be run in its own thread because it sleeps between bell strokes.
        private void PlayCode(BellCode code)
        {
            foreach (BellGroup group in code.BellGroups)
            {
                foreach (BellStroke stroke in group.Bells)
                {
                    bellPlayer.Stop();
                    Thread.Sleep(5);        // without this pause playback is crackly, with pops between bells.  It's still occasionally crackly with it...
                    bellPlayer.Play();
                    needle.ActiveState = true;
                    Thread.Sleep(stroke == BellStroke.Hold ? 2000 : 125);
                    needle.ActiveState = false;
                    Thread.Sleep(125);
                }
                Thread.Sleep(350);
            }
        }

        // The method which implements the InstrumentPlayerThread.  Plays a code, shows the code selection combo box, and exits.  The thread is restarted when the user selects the code they think they heard.
        private void RunCodePlayer()
        {
            PlayCode(ChosenCode);
            ShowCodeList(true);
        }

        // Make the code selection combo box visible safely.
        private void ShowCodeList(bool visible)
        {
            if (InvokeRequired)
            {
                Invoke(new InstrumentUiBoolCallback(ShowCodeList), visible);
                return;
            }
            if (visible)
            {
                comboCodeList.SelectedIndex = 0;
            }
            comboCodeList.Visible = visible;
        }

        private void comboModeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstrumentModeValue val = (InstrumentModeValue) comboModeSelect.SelectedItem;
            if (val.Value == InstrumentMode.Receive)
            {
                StartReceiveMode();
            }
            else
            {
                StartSendMode();
            }
        }

        // Called to switch the instrument to receive mode.  Kills the InstrumentWatcherThread, chooses a code for the user to listen to, and starts the thread to play it.
        private void StartReceiveMode()
        {
            InstrumentWatcherThread.Abort();
            InstrumentWatcherThread = new Thread(CallAttentionWatcher);
            lblInstruction.Visible = false;
            lblResult.Visible = false;
            ChosenCode = SelectRandomCode();
            InstrumentPlayerThread = new Thread(RunCodePlayer);
            InstrumentPlayerThread.Start();
            InstrumentWatcherThread.Start();
        }

        private void CallAttentionWatcher()
        {
            BellCode callAttention = new BellCode { BellGroups = new List<BellGroup> { new BellGroup { Bells = new List<BellStroke> { BellStroke.Normal } } } };

            while (true)
            {
                BlockForCodeEnd();
                if (CurrentCode != null && CurrentCode == callAttention && InstrumentPlayerThread.ThreadState != ThreadState.Running)
                {
                    InstrumentPlayerThread = new Thread(RunCodePlayer);
                    InstrumentPlayerThread.Start();
                }
                CurrentCode = new BellCode();
                RecentTapEvents.Clear();
            }
        }

        // Called in receive mode when the user picks the code they think they heard.  Scores their selection, pauses for two seconds, then re-runs receive mode.
        private void ScoreSelection()
        {
            comboCodeList.Visible = false;
            HiddenCode selCode = (HiddenCode) comboCodeList.SelectedItem;
            if (selCode.Code == ChosenCode)
            {
                correctCount++;
                CorrectUpdateScore();
            }
            else
            {
                WrongUpdateScore(InstrumentMode.Receive);
            }
            Refresh();
            Thread.Sleep(2000);
            ++doneCount;
            StartReceiveMode();
        }

        private void comboCodeList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboCodeList.SelectedIndex == 0)
            {
                return;
            }
            ScoreSelection();
        }

        private void btnCodes_Click(object sender, EventArgs e)
        {
            CodeHelplistForm codeHelplist = new CodeHelplistForm(GetSelectedCodeList());
            codeHelplist.ShowDialog(this);
        }

        private void comboListSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCodeSelectionCombo();
        }

        private void UpdateCodeSelectionCombo()
        {
            comboCodeList.Items.Clear();
            comboCodeList.Items.Add("<please select>");
            foreach (BellCode code in ((CodeList)comboListSelect.SelectedItem).Codes)
            {
                comboCodeList.Items.Add(new HiddenCode(code));
            }
            if (comboCodeList.Visible)
            {
                comboCodeList.SelectedIndex = 0;
            }
        }
    }
}
