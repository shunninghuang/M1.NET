using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace mame
{
    public partial class Taitob
    {
        public static void loop_inputports_taitob_masterw()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                Taito.sbyte2 &= ~0x04;
            }
            else
            {
                Taito.sbyte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                Taito.sbyte2 &= ~0x08;
            }
            else
            {
                Taito.sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                Taito.sbyte2 &= ~0x40;
            }
            else
            {
                Taito.sbyte2 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                Taito.sbyte2 &= unchecked((sbyte)~0x80);
            }
            else
            {
                Taito.sbyte2 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.D))
            {
                Taito.sbyte0 &= ~0x08;
            }
            else
            {
                Taito.sbyte0 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                Taito.sbyte0 &= ~0x04;
            }
            else
            {
                Taito.sbyte0 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                Taito.sbyte0 &= ~0x02;
            }
            else
            {
                Taito.sbyte0 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                Taito.sbyte0 &= ~0x01;
            }
            else
            {
                Taito.sbyte0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                Taito.sbyte0 &= ~0x10;
            }
            else
            {
                Taito.sbyte0 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                Taito.sbyte0 &= ~0x20;
            }
            else
            {
                Taito.sbyte0 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                Taito.sbyte1 &= unchecked((sbyte)~0x08);
            }
            else
            {
                Taito.sbyte1 |= unchecked((sbyte)0x08);
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                Taito.sbyte1 &= ~0x04;
            }
            else
            {
                Taito.sbyte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                Taito.sbyte1 &= ~0x02;
            }
            else
            {
                Taito.sbyte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                Taito.sbyte1 &= ~0x01;
            }
            else
            {
                Taito.sbyte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                Taito.sbyte1 &= ~0x10;
            }
            else
            {
                Taito.sbyte1 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                Taito.sbyte1 &= ~0x20;
            }
            else
            {
                Taito.sbyte1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                Taito.sbyte2 &= ~0x02;
            }
            else
            {
                Taito.sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                Taito.sbyte2 &= ~0x01;
            }
            else
            {
                Taito.sbyte2 |= 0x01;
            }
        }
        public static void loop_inputports_taitob_rambo3()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                Taito.sbyte1 |= 0x10;
            }
            else
            {
                Taito.sbyte1 &= ~0x10;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                Taito.sbyte1 |= 0x20;
            }
            else
            {
                Taito.sbyte1 &= ~0x20;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                Taito.sbyte1 &= ~0x04;
            }
            else
            {
                Taito.sbyte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                Taito.sbyte1 &= ~0x08;
            }
            else
            {
                Taito.sbyte1 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                Taito.sbyte2 &= ~0x08;
            }
            else
            {
                Taito.sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                Taito.sbyte2 &= ~0x04;
            }
            else
            {
                Taito.sbyte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                Taito.sbyte2 &= ~0x02;
            }
            else
            {
                Taito.sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                Taito.sbyte2 &= ~0x01;
            }
            else
            {
                Taito.sbyte2 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                Taito.sbyte0 &= ~0x01;
            }
            else
            {
                Taito.sbyte0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                Taito.sbyte0 &= ~0x02;
            }
            else
            {
                Taito.sbyte0 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                Taito.sbyte0 &= ~0x04;
            }
            else
            {
                Taito.sbyte0 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                Taito.sbyte2 &= unchecked((sbyte)~0x80);
            }
            else
            {
                Taito.sbyte2 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                Taito.sbyte2 &= ~0x40;
            }
            else
            {
                Taito.sbyte2 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                Taito.sbyte2 &= ~0x20;
            }
            else
            {
                Taito.sbyte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                Taito.sbyte2 &= ~0x10;
            }
            else
            {
                Taito.sbyte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                Taito.sbyte0 &= ~0x08;
            }
            else
            {
                Taito.sbyte0 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                Taito.sbyte0 &= ~0x10;
            }
            else
            {
                Taito.sbyte0 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad3))
            {
                Taito.sbyte0 &= ~0x20;
            }
            else
            {
                Taito.sbyte0 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                Taito.sbyte1 &= ~0x01;
            }
            else
            {
                Taito.sbyte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                Taito.sbyte1 &= ~0x02;
            }
            else
            {
                Taito.sbyte1 |= 0x02;
            }
        }
        public static void loop_inputports_taitob_viofight()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                Taito.sbyte2 &= ~0x04;
            }
            else
            {
                Taito.sbyte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                Taito.sbyte2 &= ~0x08;
            }
            else
            {
                Taito.sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                Taito.sbyte2 &= ~0x40;
            }
            else
            {
                Taito.sbyte2 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                Taito.sbyte2 &= unchecked((sbyte)~0x80);
            }
            else
            {
                Taito.sbyte2 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.D))
            {
                Taito.sbyte0 &= ~0x08;
            }
            else
            {
                Taito.sbyte0 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                Taito.sbyte0 &= ~0x04;
            }
            else
            {
                Taito.sbyte0 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                Taito.sbyte0 &= ~0x02;
            }
            else
            {
                Taito.sbyte0 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                Taito.sbyte0 &= ~0x01;
            }
            else
            {
                Taito.sbyte0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                Taito.sbyte0 &= ~0x10;
            }
            else
            {
                Taito.sbyte0 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                Taito.sbyte0 &= ~0x20;
            }
            else
            {
                Taito.sbyte0 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                Taito.sbyte0 &= ~0x40;
            }
            else
            {
                Taito.sbyte0 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                Taito.sbyte1 &= unchecked((sbyte)~0x08);
            }
            else
            {
                Taito.sbyte1 |= unchecked((sbyte)0x08);
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                Taito.sbyte1 &= ~0x04;
            }
            else
            {
                Taito.sbyte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                Taito.sbyte1 &= ~0x02;
            }
            else
            {
                Taito.sbyte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                Taito.sbyte1 &= ~0x01;
            }
            else
            {
                Taito.sbyte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                Taito.sbyte1 &= ~0x10;
            }
            else
            {
                Taito.sbyte1 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                Taito.sbyte1 &= ~0x20;
            }
            else
            {
                Taito.sbyte1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.NumPad3))
            {
                Taito.sbyte1 &= ~0x40;
            }
            else
            {
                Taito.sbyte1 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                Taito.sbyte2 &= ~0x02;
            }
            else
            {
                Taito.sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                Taito.sbyte2 &= ~0x01;
            }
            else
            {
                Taito.sbyte2 |= 0x01;
            }
        }
        public static void loop_inputports_taitob_silentd()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                Taito.sbyte1 &= ~0x10;
            }
            else
            {
                Taito.sbyte1 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                Taito.sbyte1 &= ~0x20;
            }
            else
            {
                Taito.sbyte1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                Taito.sbyte1 &= ~0x04;
            }
            else
            {
                Taito.sbyte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                Taito.sbyte1 &= ~0x08;
            }
            else
            {
                Taito.sbyte1 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                Taito.sbyte2 &= ~0x08;
            }
            else
            {
                Taito.sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                Taito.sbyte2 &= ~0x04;
            }
            else
            {
                Taito.sbyte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                Taito.sbyte2 &= ~0x02;
            }
            else
            {
                Taito.sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                Taito.sbyte2 &= ~0x01;
            }
            else
            {
                Taito.sbyte2 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                Taito.sbyte0 &= ~0x01;
            }
            else
            {
                Taito.sbyte0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                Taito.sbyte0 &= ~0x02;
            }
            else
            {
                Taito.sbyte0 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                Taito.sbyte0 &= ~0x04;
            }
            else
            {
                Taito.sbyte0 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                Taito.sbyte2 &= unchecked((sbyte)~0x80);
            }
            else
            {
                Taito.sbyte2 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                Taito.sbyte2 &= ~0x40;
            }
            else
            {
                Taito.sbyte2 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                Taito.sbyte2 &= ~0x20;
            }
            else
            {
                Taito.sbyte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                Taito.sbyte2 &= ~0x10;
            }
            else
            {
                Taito.sbyte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                Taito.sbyte0 &= ~0x08;
            }
            else
            {
                Taito.sbyte0 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                Taito.sbyte0 &= ~0x10;
            }
            else
            {
                Taito.sbyte0 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad3))
            {
                Taito.sbyte0 &= ~0x20;
            }
            else
            {
                Taito.sbyte0 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                Taito.sbyte1 &= ~0x01;
            }
            else
            {
                Taito.sbyte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                Taito.sbyte1 &= ~0x02;
            }
            else
            {
                Taito.sbyte1 |= 0x02;
            }
        }
        public static void loop_inputports_taitob_qzshowby()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                Taito.dswb &= unchecked((byte)~0x10);
            }
            else
            {
                Taito.dswb |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                Taito.dswb &= unchecked((byte)~0x20);
            }
            else
            {
                Taito.dswb |= 0x20;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                Taito.sbyte0 &= ~0x10;
            }
            else
            {
                Taito.sbyte0 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                Taito.sbyte0 &= ~0x20;
            }
            else
            {
                Taito.sbyte0 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                Taito.sbyte2 &= ~0x01;
            }
            else
            {
                Taito.sbyte2 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                Taito.sbyte2 &= ~0x02;
            }
            else
            {
                Taito.sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                Taito.sbyte2 &= ~0x04;
            }
            else
            {
                Taito.sbyte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.U))
            {
                Taito.sbyte2 &= ~0x08;
            }
            else
            {
                Taito.sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                Taito.sbyte2 &= ~0x10;
            }
            else
            {
                Taito.sbyte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                Taito.sbyte2 &= ~0x20;
            }
            else
            {
                Taito.sbyte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.NumPad3))
            {
                Taito.sbyte2 &= ~0x40;
            }
            else
            {
                Taito.sbyte2 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.NumPad4))
            {
                Taito.sbyte2 &= unchecked((sbyte)~0x80);
            }
            else
            {
                Taito.sbyte2 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.R))
            {
                Taito.sbyte0 &= ~0x01;
            }
            else
            {
                Taito.sbyte0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                Taito.sbyte0 &= ~0x02;
            }
            else
            {
                Taito.sbyte0 |= 0x02;
            }
        }
        public static void loop_inputports_taitob_pbobble()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                Taito.dswb &= unchecked((byte)~0x10);
            }
            else
            {
                Taito.dswb |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                Taito.dswb &= unchecked((byte)~0x20);
            }
            else
            {
                Taito.dswb |= 0x20;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                Taito.sbyte0 &= ~0x10;
            }
            else
            {
                Taito.sbyte0 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                Taito.sbyte0 &= ~0x20;
            }
            else
            {
                Taito.sbyte0 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                Taito.sbyte2 &= ~0x08;
            }
            else
            {
                Taito.sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                Taito.sbyte2 &= ~0x04;
            }
            else
            {
                Taito.sbyte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                Taito.sbyte2 &= ~0x02;
            }
            else
            {
                Taito.sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                Taito.sbyte2 &= ~0x01;
            }
            else
            {
                Taito.sbyte2 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                Taito.sbyte1 &= ~0x01;
            }
            else
            {
                Taito.sbyte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                Taito.sbyte1 &= ~0x02;
            }
            else
            {
                Taito.sbyte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                Taito.sbyte1 &= ~0x04;
            }
            else
            {
                Taito.sbyte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                Taito.sbyte2 &= unchecked((sbyte)~0x80);
            }
            else
            {
                Taito.sbyte2 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                Taito.sbyte2 &= ~0x40;
            }
            else
            {
                Taito.sbyte2 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                Taito.sbyte2 &= ~0x20;
            }
            else
            {
                Taito.sbyte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                Taito.sbyte2 &= ~0x10;
            }
            else
            {
                Taito.sbyte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                Taito.sbyte1 &= ~0x10;
            }
            else
            {
                Taito.sbyte1 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                Taito.sbyte1 &= ~0x20;
            }
            else
            {
                Taito.sbyte1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.NumPad3))
            {
                Taito.sbyte1 &= ~0x40;
            }
            else
            {
                Taito.sbyte1 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                Taito.sbyte0 &= ~0x01;
            }
            else
            {
                Taito.sbyte0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                Taito.sbyte0 &= ~0x02;
            }
            else
            {
                Taito.sbyte0 |= 0x02;
            }
        }
        public static void loop_inputports_taitob_sbm()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                Taito.sbyte2 &= ~0x04;
            }
            else
            {
                Taito.sbyte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                Taito.sbyte2 &= ~0x08;
            }
            else
            {
                Taito.sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                Taito.sbyte1 &= ~0x01;
            }
            else
            {
                Taito.sbyte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                Taito.sbyte1 &= ~0x02;
            }
            else
            {
                Taito.sbyte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                Taito.sbyte0 &= ~0x08;
            }
            else
            {
                Taito.sbyte0 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                Taito.sbyte0 &= ~0x04;
            }
            else
            {
                Taito.sbyte0 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                Taito.sbyte0 &= ~0x02;
            }
            else
            {
                Taito.sbyte0 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                Taito.sbyte0 &= ~0x01;
            }
            else
            {
                Taito.sbyte0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                Taito.sbyte2 &= ~0x10;
            }
            else
            {
                Taito.sbyte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                Taito.sbyte2 |= 0x20;
            }
            else
            {
                Taito.sbyte2 &= ~0x20;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                Taito.sbyte2 |= 0x40;                
            }
            else
            {
                Taito.sbyte2 &= ~0x40;
            }
            if (Keyboard.IsPressed(Key.U))
            {
                Taito.sbyte2 &= unchecked((sbyte)~0x80);
            }
            else
            {
                Taito.sbyte2 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                Taito.sbyte1 &= unchecked((sbyte)~0x80);
            }
            else
            {
                Taito.sbyte1 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                Taito.sbyte0 &= ~0x40;
            }
            else
            {
                Taito.sbyte0 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                Taito.sbyte0 &= ~0x20;
            }
            else
            {
                Taito.sbyte0 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                Taito.sbyte0 &= ~0x10;
            }
            else
            {
                Taito.sbyte0 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                Taito.sbyte2 &= ~0x02;
            }
            else
            {
                Taito.sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                Taito.sbyte2 &= ~0x01;
            }
            else
            {
                Taito.sbyte2 |= 0x01;
            }
        }
        public static void loop_inputports_taitob_realpunc()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                Taito.sbyte0 &= ~0x40;
            }
            else
            {
                Taito.sbyte0 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                Taito.sbyte0 &= unchecked((sbyte)~0x80);
            }
            else
            {
                Taito.sbyte0 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                Taito.sbyte0 &= ~0x01;
            }
            else
            {
                Taito.sbyte0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                Taito.sbyte0 &= ~0x02;
            }
            else
            {
                Taito.sbyte0 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                Taito.sbyte2 &= ~0x01;
            }
            else
            {
                Taito.sbyte2 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                Taito.sbyte2 &= ~0x10;
            }
            else
            {
                Taito.sbyte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                Taito.sbyte2 &= ~0x20;
            }
            else
            {
                Taito.sbyte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.U))
            {
                Taito.sbyte2 &= ~0x40;
            }
            else
            {
                Taito.sbyte2 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                Taito.sbyte0 &= ~0x08;
            }
            else
            {
                Taito.sbyte0 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                Taito.sbyte0 &= ~0x10;
            }
            else
            {
                Taito.sbyte0 |= 0x10;
            }
        }
        public static void record_port()
        {
            if (Taito.sbyte0 != Taito.sbyte0_old || Taito.sbyte1 != Taito.sbyte1_old || Taito.sbyte2 != Taito.sbyte2_old || Taito.sbyte3 != Taito.sbyte3_old || Taito.sbyte4 != Taito.sbyte4_old || Taito.sbyte5 != Taito.sbyte5_old)
            {
                Taito.sbyte0_old = Taito.sbyte0;
                Taito.sbyte1_old = Taito.sbyte1;
                Taito.sbyte2_old = Taito.sbyte2;
                Taito.sbyte3_old = Taito.sbyte3;
                Taito.sbyte4_old = Taito.sbyte4;
                Taito.sbyte5_old = Taito.sbyte5;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(Taito.sbyte0);
                Mame.bwRecord.Write(Taito.sbyte1);
                Mame.bwRecord.Write(Taito.sbyte2);
                Mame.bwRecord.Write(Taito.sbyte3);
                Mame.bwRecord.Write(Taito.sbyte4);
                Mame.bwRecord.Write(Taito.sbyte5);
            }
        }
        public static void replay_port()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    Taito.sbyte0_old = Mame.brRecord.ReadSByte();
                    Taito.sbyte1_old = Mame.brRecord.ReadSByte();
                    Taito.sbyte2_old = Mame.brRecord.ReadSByte();
                    Taito.sbyte3_old = Mame.brRecord.ReadSByte();
                    Taito.sbyte4_old = Mame.brRecord.ReadSByte();
                    Taito.sbyte5_old = Mame.brRecord.ReadSByte();
                }
                catch
                {
                    Mame.playState = Mame.PlayState.PLAY_REPLAYEND;
                    //Mame.mame_pause(true);
                }
                Inptport.bReplayRead = false;
            }
            if (Video.screenstate.frame_number == Video.frame_number_obj)
            {
                Taito.sbyte0 = Taito.sbyte0_old;
                Taito.sbyte1 = Taito.sbyte1_old;
                Taito.sbyte2 = Taito.sbyte2_old;
                Taito.sbyte3 = Taito.sbyte3_old;
                Taito.sbyte4 = Taito.sbyte4_old;
                Taito.sbyte5 = Taito.sbyte5_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
        public static void record_port_pbobble()
        {
            if (Taito.dswb != Taito.dswb_old || Taito.sbyte0 != Taito.sbyte0_old || Taito.sbyte1 != Taito.sbyte1_old || Taito.sbyte2 != Taito.sbyte2_old || Taito.sbyte3 != Taito.sbyte3_old || Taito.sbyte4 != Taito.sbyte4_old || Taito.sbyte5 != Taito.sbyte5_old)
            {
                Taito.dswb_old = Taito.dswb;
                Taito.sbyte0_old = Taito.sbyte0;
                Taito.sbyte1_old = Taito.sbyte1;
                Taito.sbyte2_old = Taito.sbyte2;
                Taito.sbyte3_old = Taito.sbyte3;
                Taito.sbyte4_old = Taito.sbyte4;
                Taito.sbyte5_old = Taito.sbyte5;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(Taito.dswb);
                Mame.bwRecord.Write(Taito.sbyte0);
                Mame.bwRecord.Write(Taito.sbyte1);
                Mame.bwRecord.Write(Taito.sbyte2);
                Mame.bwRecord.Write(Taito.sbyte3);
                Mame.bwRecord.Write(Taito.sbyte4);
                Mame.bwRecord.Write(Taito.sbyte5);
            }
        }
        public static void replay_port_pbobble()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    Taito.dswb_old = Mame.brRecord.ReadByte();
                    Taito.sbyte0_old = Mame.brRecord.ReadSByte();
                    Taito.sbyte1_old = Mame.brRecord.ReadSByte();
                    Taito.sbyte2_old = Mame.brRecord.ReadSByte();
                    Taito.sbyte3_old = Mame.brRecord.ReadSByte();
                    Taito.sbyte4_old = Mame.brRecord.ReadSByte();
                    Taito.sbyte5_old = Mame.brRecord.ReadSByte();
                }
                catch
                {
                    Mame.playState = Mame.PlayState.PLAY_REPLAYEND;
                    //Mame.mame_pause(true);
                }
                Inptport.bReplayRead = false;
            }
            if (Video.screenstate.frame_number == Video.frame_number_obj)
            {
                Taito.dswb = Taito.dswb_old;
                Taito.sbyte0 = Taito.sbyte0_old;
                Taito.sbyte1 = Taito.sbyte1_old;
                Taito.sbyte2 = Taito.sbyte2_old;
                Taito.sbyte3 = Taito.sbyte3_old;
                Taito.sbyte4 = Taito.sbyte4_old;
                Taito.sbyte5 = Taito.sbyte5_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
    }
}
