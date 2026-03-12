using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace mame
{
    public partial class Tad
    {
        public static void loop_inputports_tad_toki()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                bytec |= unchecked((byte)~0x01);
            }
            else
            {
                bytec &= 0x01;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                bytec |= unchecked((byte)~0x02);
            }
            else
            {
                bytec &= 0x02;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                sbyte2 &= ~0x08;
            }
            else
            {
                sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                sbyte2 &= ~0x10;
            }
            else
            {
                sbyte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                short1 &= ~0x0008;
            }
            else
            {
                short1 |= 0x0008;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                short1 &= ~0x0004;
            }
            else
            {
                short1 |= 0x0004;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                short1 &= ~0x0002;
            }
            else
            {
                short1 |= 0x0002;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                short1 &= ~0x0001;
            }
            else
            {
                short1 |= 0x0001;
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
                short1 &= ~0x0800;
            }
            else
            {
                short1 |= 0x0800;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                short1 &= ~0x0400;
            }
            else
            {
                short1 |= 0x0400;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                short1 &= ~0x0200;
            }
            else
            {
                short1 |= 0x0200;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                short1 &= ~0x0100;
            }
            else
            {
                short1 |= 0x0100;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                short1 &= ~0x1000;
            }
            else
            {
                short1 |= 0x1000;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                short1 &= ~0x2000;
            }
            else
            {
                short1 |= 0x2000;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                sbyte2 &= ~0x04;
            }
            else
            {
                sbyte2 |= 0x04;
            }
            /*if (Keyboard.IsPressed(Key.T))
            {
                sbyte0 &= ~0x40;
            }
            else
            {
                sbyte0 |= 0x40;
            }*/
        }
        public static void loop_inputports_tad_tokib()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                sbyte2 &= ~0x01;
            }
            else
            {
                sbyte2 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                sbyte2 &= ~0x02;
            }
            else
            {
                sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                sbyte2 &= ~0x08;
            }
            else
            {
                sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                sbyte2 &= ~0x10;
            }
            else
            {
                sbyte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                short1 &= ~0x0008;
            }
            else
            {
                short1 |= 0x0008;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                short1 &= ~0x0004;
            }
            else
            {
                short1 |= 0x0004;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                short1 &= ~0x0002;
            }
            else
            {
                short1 |= 0x0002;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                short1 &= ~0x0001;
            }
            else
            {
                short1 |= 0x0001;
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
                short1 &= ~0x0800;
            }
            else
            {
                short1 |= 0x0800;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                short1 &= ~0x0400;
            }
            else
            {
                short1 |= 0x0400;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                short1 &= ~0x0200;
            }
            else
            {
                short1 |= 0x0200;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                short1 &= ~0x0100;
            }
            else
            {
                short1 |= 0x0100;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                short1 &= ~0x1000;
            }
            else
            {
                short1 |= 0x1000;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                short1 &= ~0x2000;
            }
            else
            {
                short1 |= 0x2000;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                sbyte2 &= ~0x04;
            }
            else
            {
                sbyte2 |= 0x04;
            }
            /*if (Keyboard.IsPressed(Key.T))
            {
                sbyte0 &= ~0x40;
            }
            else
            {
                sbyte0 |= 0x40;
            }*/
        }
        public static void record_port_tad_toki()
        {
            if (bytec != bytec_old || short1 != short1_old || sbyte2 != sbyte2_old)
            {
                bytec_old = bytec;
                short1_old = short1;
                sbyte2_old = sbyte2;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(bytec);
                Mame.bwRecord.Write(short1);
                Mame.bwRecord.Write(sbyte2);
            }
        }
        public static void replay_port_tad_toki()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    bytec_old = Mame.brRecord.ReadByte();
                    short1_old = Mame.brRecord.ReadInt16();
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
                bytec = bytec_old;
                short1 = short1_old;
                sbyte2 = sbyte2_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
        public static void record_port_tad_tokib()
        {
            if (short1 != short1_old || sbyte2 != sbyte2_old)
            {
                short1_old = short1;
                sbyte2_old = sbyte2;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(short1);
                Mame.bwRecord.Write(sbyte2);
            }
        }
        public static void replay_port_tad_tokib()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    short1_old = Mame.brRecord.ReadInt16();
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
                short1 = short1_old;
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
