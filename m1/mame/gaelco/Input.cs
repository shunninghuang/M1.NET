using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace mame
{
    public partial class Gaelco
    {
        public static void loop_inputports_gaelco()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                sbyte1 &= ~0x40;
            }
            else
            {
                sbyte1 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                sbyte1 &= unchecked((sbyte)~0x80);
            }
            else
            {
                sbyte1 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                sbyte2 &= ~0x40;
            }
            else
            {
                sbyte2 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                sbyte2 &= unchecked((sbyte)~0x80);
            }
            else
            {
                sbyte2 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.D))
            {
                sbyte1 &= ~0x04;
            }
            else
            {
                sbyte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                sbyte1 &= ~0x08;
            }
            else
            {
                sbyte1 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                sbyte1 &= ~0x02;
            }
            else
            {
                sbyte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                sbyte1 &= ~0x01;
            }
            else
            {
                sbyte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                sbyte1 &= ~0x20;
            }
            else
            {
                sbyte1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                sbyte1 &= ~0x10;
            }
            else
            {
                sbyte1 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                sbyte2 &= ~0x04;
            }
            else
            {
                sbyte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                sbyte2 &= ~0x08;
            }
            else
            {
                sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                sbyte2 &= ~0x02;
            }
            else
            {
                sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                sbyte2 &= ~0x01;
            }
            else
            {
                sbyte2 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                sbyte2 &= ~0x20;
            }
            else
            {
                sbyte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                sbyte2 &= ~0x10;
            }
            else
            {
                sbyte2 |= 0x10;
            }
            /*if (Keyboard.IsPressed(Key.R))
            {
                sbyte0 &= ~0x04;
            }
            else
            {
                sbyte0 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                sbyte0 &= ~0x40;
            }
            else
            {
                sbyte0 |= 0x40;
            }*/
        }
        public static void loop_inputports_gaelco_lastkm()
        {
            if (Keyboard.IsPressed(Key.D1))
            {
                sbyte1 &= ~0x40;
            }
            else
            {
                sbyte1 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                sbyte1 &= unchecked((sbyte)~0x80);
            }
            else
            {
                sbyte1 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.J))
            {
                sbyte1 &= ~0x20;
            }
            else
            {
                sbyte1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                sbyte1 &= ~0x10;
            }
            else
            {
                sbyte1 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                sbyte1 &= ~0x02;
            }
            else
            {
                sbyte1 |= 0x02;
            }
        }
        public static void record_port_gaelco()
        {
            if (sbyte1 != sbyte1_old || sbyte2 != sbyte2_old)
            {
                sbyte1_old = sbyte1;
                sbyte2_old = sbyte2;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(sbyte1);
                Mame.bwRecord.Write(sbyte2);
            }
        }
        public static void replay_port_gaelco()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    sbyte1_old = Mame.brRecord.ReadSByte();
                    sbyte2_old = Mame.brRecord.ReadSByte();
                }
                catch
                {
                    Mame.playState = Mame.PlayState.PLAY_REPLAYEND;
                }
                Inptport.bReplayRead = false;
            }
            if (Video.screenstate.frame_number == Video.frame_number_obj)
            {
                sbyte1 = sbyte1_old;
                sbyte2 = sbyte2_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
    }
}
