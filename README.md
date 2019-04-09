# bellcode-tester
Test your knowledge of British railway signalling bell codes!

Traditionally on the British railway system - by which I mean about 1870 to date - signalmen and signallers communicated with each other using single-stroke bells of different pitches, with a semi-standardised list of "bell codes" used to send specific messages.  This is a Windows desktop app to help you learn these bell codes and test your knowledge of them.

## Requirements

The code for this was originally written a few years ago, and it is a bit scrappy in a variety of ways.  

Currently it uses the .NET Framework 3.5.  The MSI output file is built using [the Wix Toolset v3.11](http://wixtoolset.org/).

## Operation

The program builds a not-quite-standalone .exe, or a .msi installation file.  A basic non-technical documentation file is included&mdash;aimed at railway enthusiasts rather than developers.  The lists of codes are loaded from XML

The program imitates (in a very basic way) a Tyers No. 9 Key Token block instrument, with a brass plunger and a telegraph needle that moves when the plunger is pressed, and which is theoretically connected to another identical instrument a few miles away, with another signaller standing at it and operating it.  Some operations with the Key Token instrument require the signaller to hold down the plunger, activating a solenoid in the connected block instrument so that the other signaller can take a token out of the connected instrument.  This causes the telegraph needle on the first instrument to characteristically "flick" twice.  This behaviour is implemented in the program - if the program asks you to send a "line clear for..." code (as opposed to an "Is the line clear for...?" code) you are expected to hold down the plunger until you see the needle flicks.

You can see a picture of an actual Tyers Key Token instrument [on this web page which explains a little more about how they are used](https://www.svrwiki.com/Single_line_working_using_tokens). Note that in the program the relative positions of needle and plunger have been shuffled around a little bit.
