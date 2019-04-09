using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace BellTest
{
    /// <summary>
    /// The on-screen representation of a galvanometer needle.
    /// </summary>
    public partial class Needle : UserControl
    {
        private delegate void RemoteNeedleCallback();

        // Drawing parameters.  Sizes are fractions of the shortest dimension of the control; needleRotation is the deflection in radians.
        private float _bezelSize = 0.9f;
        private float _needleSize = 0.7f;
        private float _needleRotation = Properties.Settings.Default.NeedleDeflection;

        private Thread _remoteNeedleThread;

        [DefaultValue(0.9f)]
        public float BezelSize
        {
            get { return _bezelSize; }
            set
            {
                _bezelSize = value;
                ComputeAbsoluteCoords();
            }
        }

        [DefaultValue(0.7f)]
        public float NeedleSize
        {
            get { return _needleSize; }
            set
            {
                _needleSize = value;
                ComputeAbsoluteCoords();
            }
        }
        
        [DefaultValue(0.2617993f)]
        public float NeedleRotation
        {
            get { return _needleRotation; }
            set
            {
                _needleRotation = value;
                ComputeAbsoluteCoords();
            }
        }

        private bool _state;

        [DefaultValue(false)]
        public bool ActiveState
        {
            get { return _state; }
            set
            {
                _state = value;
                if (InvokeRequired)
                {
                    Invoke(new RemoteNeedleCallback(Refresh));
                }
                else
                {
                    Refresh();
                }
            }
        }

        private RectangleF BezelBoundingRect { get; set; }

        private PointF[] NeedleNormalCoords { get; set; }
        private PointF[] NeedleActiveCoords { get; set; }

        private RectangleF PivotBoundingRect { get; set; }

        public Needle()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ComputeAbsoluteCoords();
        }

        // Compute the coordinates for drawing the various parts of the needle when active and idle based on the control size.
        private void ComputeAbsoluteCoords()
        {
            float smallestDim = Size.Height < Size.Width ? Size.Height : Size.Width;
            float bezelSizeAbsolute = smallestDim * BezelSize;
            BezelBoundingRect = new RectangleF(new PointF((Size.Width - bezelSizeAbsolute) / 2.0f, (Size.Height - bezelSizeAbsolute) / 2.0f), new SizeF(bezelSizeAbsolute, bezelSizeAbsolute));

            float needleLengthAbsolute = smallestDim * NeedleSize;
            float needleWidthAbsolute = needleLengthAbsolute / 8.0f;

            NeedleNormalCoords = new PointF[]
                {
                    new PointF((Size.Width - needleWidthAbsolute) / 2.0f, Size.Height / 2.0f),
                    new PointF(Size.Width / 2.0f, (Size.Height - needleLengthAbsolute) / 2.0f),
                    new PointF((Size.Width + needleWidthAbsolute) / 2.0f, Size.Height / 2.0f),
                    new PointF(Size.Width / 2.0f, (Size.Height + needleLengthAbsolute) / 2.0f)
                };

            float acuteOffsetX = (float) (needleLengthAbsolute * Math.Sin(NeedleRotation)) / 2.0f;
            float acuteOffsetY = (needleLengthAbsolute - (float) (needleLengthAbsolute * Math.Cos(NeedleRotation))) / 2.0f;
            float obtuseOffsetX = (needleWidthAbsolute - (float) (needleWidthAbsolute * Math.Cos(NeedleRotation))) / 2.0f;
            float obtuseOffsetY = (float) (needleWidthAbsolute * Math.Sin(NeedleRotation)) / 2.0f;

            NeedleActiveCoords = new PointF[]
                {
                    new PointF(NeedleNormalCoords[0].X + obtuseOffsetX, NeedleNormalCoords[0].Y - obtuseOffsetY),
                    new PointF(NeedleNormalCoords[1].X + acuteOffsetX, NeedleNormalCoords[1].Y + acuteOffsetY),
                    new PointF(NeedleNormalCoords[2].X - obtuseOffsetX, NeedleNormalCoords[2].Y + obtuseOffsetY),
                    new PointF(NeedleNormalCoords[3].X - acuteOffsetX, NeedleNormalCoords[3].Y - acuteOffsetY)
                };

            float pivotLength = needleWidthAbsolute * 3.5f;
            float pivotHeight = needleWidthAbsolute;

            PivotBoundingRect = new RectangleF((Size.Width - pivotLength) / 2f, (Size.Height - pivotHeight) / 2f, pivotLength, pivotHeight);
        }

        private void Needle_Resize(object sender, EventArgs e)
        {
            ComputeAbsoluteCoords();
        }

        // Draw the needle
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Pen bezelPen = new Pen(Color.Black, 2.0f);
            Brush bezelBrush = new SolidBrush(Color.White);
            e.Graphics.FillEllipse(bezelBrush, BezelBoundingRect);
            e.Graphics.DrawEllipse(bezelPen, BezelBoundingRect);

            Brush needleBrush = new SolidBrush(Color.Black);
            e.Graphics.FillPolygon(needleBrush, ActiveState ? NeedleActiveCoords : NeedleNormalCoords);

            Brush pivotBrush = new SolidBrush(Color.Goldenrod);
            Pen pivotPen = new Pen(Color.Black, 1.5f);

            e.Graphics.FillRectangle(pivotBrush, PivotBoundingRect);
            e.Graphics.DrawRectangle(pivotPen, PivotBoundingRect.X, PivotBoundingRect.Y, PivotBoundingRect.Width, PivotBoundingRect.Height);
        }

        /// <summary>
        /// Start a new thread that, if the plunger is still held down after 0.4s, deflects the needle as if a token as been withdrawn.
        /// </summary>
        internal void TriggerTokenRelease()
        {
            _remoteNeedleThread = new Thread(WithdrawToken);
            _remoteNeedleThread.Start();
        }

        /// <summary>
        /// Start a new thread that, if the plunger is still held down after 0.75s, puts the needle to vertical as if the machine has gone dead.
        /// </summary>
        internal void TriggerSwitchOut()
        {
            _remoteNeedleThread = new Thread(WaitThenSwitchOut);
            _remoteNeedleThread.Start();
        }

        /// <summary>
        /// A method that stops the thread that is waiting to withdraw a token or switch out if the plunger is released too early.
        /// </summary>
        internal void LocalPlungerReleased()
        {
            if (_remoteNeedleThread == null)
            {
                return;
            }
            _remoteNeedleThread.Abort();
            _remoteNeedleThread = null;
        }

        public void WaitThenSwitchOut()
        {
            Thread.Sleep(750);
            RemoteNeedleOff();
        }

        /// <summary>
        /// Safely set the needle to normal.
        /// </summary>
        internal void RemoteNeedleOff()
        {
            if (InvokeRequired)
            {
                Invoke(new RemoteNeedleCallback(RemoteNeedleOff));
                return;
            }
            _state = false;
            Refresh();
        }

        /// <summary>
        /// Safely set the needle to active.
        /// </summary>
        internal void RemoteNeedleOn()
        {
            if (InvokeRequired)
            {
                Invoke(new RemoteNeedleCallback(RemoteNeedleOn));
                return;
            }
            _state = true;
            Refresh();
        }

        public void WithdrawToken()
        {
            Thread.Sleep(400);
            RemoteNeedleOff();
            Thread.Sleep(125);
            RemoteNeedleOn();
            Thread.Sleep(125);
            RemoteNeedleOff();
            Thread.Sleep(125);
            RemoteNeedleOn();
        }
    }
}
