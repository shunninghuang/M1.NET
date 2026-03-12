using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Tad
    {
        public static byte[] main2sub=new byte[2], sub2main=new byte[2];
        public static int main2sub_pending, sub2main_pending;
        public static int sound_cpu, irq1, irq2;
        public static void update_irq_lines(int param)
        {
            switch (param)
            {
                case 0:
                    irq1 = irq2 = 0xff;
                    break;

                case 1:
                    irq1 = 0xd7;
                    break;

                case 2:
                    irq1 = 0xff;
                    break;

                case 3:
                    irq2 = 0xdf;
                    break;

                case 4:
                    irq2 = 0xff;
                    break;
            }
            if ((irq1 & irq2) == 0xff)
            {
                Cpuint.cpunum_set_input_line(sound_cpu, 0, LineState.CLEAR_LINE);
            }
            else
            {
                Cpuint.cpunum_set_input_line_and_vector2(sound_cpu, 0, LineState.ASSERT_LINE, irq1 & irq2);
            }
        }
        public static void seibu_irq_clear_w()
        {
            update_irq_lines(0);
        }
        public static void seibu_rst10_ack_w()
        {

        }
        public static void seibu_rst18_ack_w()
        {
            update_irq_lines(4);
        }
        public static void seibu_ym3812_irqhandler(LineState linestate)
        {
            update_irq_lines(linestate != 0 ? 1 : 2);
        }
        public static void machine_reset_seibu_sound()
        {
            int romlength = Memory.audiorom.Length;
            sound_cpu = 1;
            update_irq_lines(0);
            if (romlength > 0x10000)
            {
                basebanksnd = 0x10000;
            }
        }
        public static void seibu_bank_w(byte data)
        {
            basebanksnd = 0x10000 + (data & 0x01) * 0x8000;
        }
        public static void seibu_coin_w()
        {

        }
        public static byte seibu_soundlatch_r(int offset)
        {
            return main2sub[offset];
        }
        public static byte seibu_main_data_pending_r()
        {
            return (byte)(sub2main_pending != 0 ? 1 : 0);
        }
        public static void seibu_main_data_w(int offset, byte data)
        {
            sub2main[offset] = data;
        }
        public static void seibu_pending_w()
        {
            main2sub_pending = 0;
            sub2main_pending = 1;
        }
        public static ushort seibu_main_word_r(int offset)
        {
            switch (offset)
            {
                case 2:
                case 3:
                    return sub2main[offset - 2];
                case 5:
                    return (ushort)(main2sub_pending != 0 ? 1 : 0);
                default:
                    return 0xffff;
            }
        }
        public static void seibu_main_word_w(int offset, ushort data)
        {
            //if (ACCESSING_BITS_0_7)
            {
                switch (offset)
                {
                    case 0:
                    case 1:
                        main2sub[offset] = (byte)data;
                        break;
                    case 4:
                        update_irq_lines(3);
                        break;
                    case 6:
                        sub2main_pending = 0;
                        main2sub_pending = 1;
                        break;
                    default:
                        break;
                }
            }
        }
        public static void seibu_main_word_w2(int offset, byte data)
        {
            //if (ACCESSING_BITS_0_7)
            {
                switch (offset)
                {
                    case 0:
                    case 1:
                        main2sub[offset] = data;
                        break;
                    case 4:
                        update_irq_lines(3);
                        break;
                    case 6:
                        sub2main_pending = 0;
                        main2sub_pending = 1;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
