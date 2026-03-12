using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace mame
{
    public partial class Megasys1
    {
        public static void loop_inputports_megasys1_64street()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                shorts &= ~0x0040;
            }
            else
            {
                shorts |= 0x0040;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                shorts &= ~0x0080;
            }
            else
            {
                shorts |= 0x0080;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                shorts &= ~0x0001;
            }
            else
            {
                shorts |= 0x0001;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                shorts &= ~0x0002;
            }
            else
            {
                shorts |= 0x0002;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                short1 &= ~0x0001;
            }
            else
            {
                short1 |= 0x0001;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                short1 &= ~0x0002;
            }
            else
            {
                short1 |= 0x0002;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                short1 &= ~0x0004;
            }
            else
            {
                short1 |= 0x0004;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                short1 &= ~0x0008;
            }
            else
            {
                short1 |= 0x0008;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                short1 &= ~0x0010;
            }
            else
            {
                short1 |= 0x0010;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                short1 &= ~0x0020;
            }
            else
            {
                short1 |= 0x0020;
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                short2 &= ~0x0001;
            }
            else
            {
                short2 |= 0x0001;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                short2 &= ~0x0002;
            }
            else
            {
                short2 |= 0x0002;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                short2 &= ~0x0004;
            }
            else
            {
                short2 |= 0x0004;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                short2 &= ~0x0008;
            }
            else
            {
                short2 |= 0x0008;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                short2 &= ~0x0010;
            }
            else
            {
                short2 |= 0x0010;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                short2 &= ~0x0020;
            }
            else
            {
                short2 |= 0x0020;
            }
        }
        public static void loop_inputports_megasys1_hayaosi1()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                shorts &= ~0x0040;
            }
            else
            {
                shorts |= 0x0040;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                shorts &= ~0x0080;
            }
            else
            {
                shorts |= 0x0080;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                shorts &= ~0x0001;
            }
            else
            {
                shorts |= 0x0001;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                shorts &= ~0x0002;
            }
            else
            {
                shorts |= 0x0002;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                short1 &= ~0x0008;
            }
            else
            {
                short1 |= 0x0008;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                short2 &= ~0x0008;
            }
            else
            {
                short2 |= 0x0008;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                short1 &= ~0x0004;
            }
            else
            {
                short1 |= 0x0004;
            }
            if (Keyboard.IsPressed(Key.U))
            {
                short2 &= ~0x0004;
            }
            else
            {
                short2 |= 0x0004;
            }
            if (Keyboard.IsPressed(Key.I))
            {
                short1 &= ~0x0040;
            }
            else
            {
                short1 |= 0x0040;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                short1 &= ~0x0002;
            }
            else
            {
                short1 |= 0x0002;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                short2 &= ~0x0002;
            }
            else
            {
                short2 |= 0x0002;
            }
            if (Keyboard.IsPressed(Key.NumPad3))
            {
                short1 &= ~0x0001;
            }
            else
            {
                short1 |= 0x0001;
            }
            if (Keyboard.IsPressed(Key.NumPad4))
            {
                short2 &= ~0x0001;
            }
            else
            {
                short2 |= 0x0001;
            }
            if (Keyboard.IsPressed(Key.NumPad5))
            {
                short2 &= ~0x0040;
            }
            else
            {
                short2 |= 0x0040;
            }
        }
        public static void record_port()
        {
            if (shorts != shorts_old || short1 != short1_old || short2 != shorts_old)
            {
                shorts_old = shorts;
                short1_old = short1;
                short2_old = short2;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(shorts);
                Mame.bwRecord.Write(short1);
                Mame.bwRecord.Write(short2);
            }
        }
        public static void replay_port()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    shorts_old = Mame.brRecord.ReadInt16();
                    short1_old = Mame.brRecord.ReadInt16();
                    short2_old = Mame.brRecord.ReadInt16();
                }
                catch
                {
                    Mame.playState = Mame.PlayState.PLAY_REPLAYEND;
                }
                Inptport.bReplayRead = false;
            }
            if (Video.screenstate.frame_number == Video.frame_number_obj)
            {
                shorts = shorts_old;
                short1 = short1_old;
                short2 = short2_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
    }
}
