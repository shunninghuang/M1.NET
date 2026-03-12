using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Megasys1
    {
        public static ushort[][] megasys1_scrollram = new ushort[3][];
        public static ushort[] megasys1_objectram;
        public static ushort[] megasys1_scrollx = new ushort[3], megasys1_scrolly = new ushort[3],megasys1_scroll_flag = new ushort[3];
        public static ushort megasys1_active_layers,megasys1_sprite_bank,megasys1_screen_flag, megasys1_sprite_flag,ip_latched;
        public static int megasys1_bits_per_color_code, hardware_type_z, mainram_offset;
        public static int[] megasys1_8x8_scroll_factor = new int[3], megasys1_16x16_scroll_factor = new int[3];
        public static ushort[] ip_select_values = new ushort[7];
        public static byte megasys1_ignore_oki_status = 0;
        public static byte dsw1, dsw2;
        public static byte[][] scrollrom;
        public static byte[] spritesrom, promsrom;
        public static void Megasys1Init()
        {
            int i;
            Machine.bRom = true;            
            for (i = 0; i < 3; i++)
            {
                megasys1_scrollram[i] = new ushort[0x2000];
            }
            megasys1_objectram = new ushort[0x1000];
            Generic.paletteram16 = new ushort[0x400];
            scrollrom = new byte[3][];
            switch (Machine.sName)
            {
                case "lomakai"://Z
                case "makaiden":
                    Memory.mainram = new byte[0x10000];
                    Memory.audioram = new byte[0x10000];
                    Memory.mainrom = new byte[0x10000]; //Machine.GetRom("maincpu.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    scrollrom[0] = new byte[0x10000]; //Machine.GetRom("scroll0.rom");
                    scrollrom[1] = new byte[0x10000]; //Machine.GetRom("scroll1.rom");
                    spritesrom = new byte[0x10000]; //Machine.GetRom("sprites.rom");
                    promsrom = new byte[0x10000]; //Machine.GetRom("proms.rom");
                    if (Memory.mainrom == null || Memory.audiorom == null || scrollrom[0] == null || scrollrom[1] == null || spritesrom == null || promsrom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "p47"://A
                case "p47j":
                case "p47je":
                case "kickoff":
                case "tshingen":
                case "tshingena":
                case "kazan":
                case "iganinju":
                case "astyanax":
                case "lordofk":
                case "hachoo":
                case "jitsupro":
                case "plusalph":
                case "stdragon":
                case "stdragona":
                case "stdragonb":
                case "rodland":
                case "rodlandj":
                case "rittam":
                case "rodlandjb":
                case "phantasm":
                case "edfp":
                case "soldam":
                case "soldamj":
                    Memory.mainram = new byte[0x10000];
                    Memory.audioram = new byte[0x20000];
                    Memory.mainrom = new byte[0x10000]; //Machine.GetRom("maincpu.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    scrollrom[0] = new byte[0x10000]; //Machine.GetRom("scroll0.rom");
                    scrollrom[1] = new byte[0x10000]; //Machine.GetRom("scroll1.rom");
                    scrollrom[2] = new byte[0x10000]; //Machine.GetRom("scroll2.rom");
                    spritesrom = new byte[0x10000]; //Machine.GetRom("sprites.rom");
                    OKI6295.oo1[0] = new OKI6295();
                    OKI6295.oo1[0].okirom = Machine.GetRom("oki1.rom");
                    OKI6295.oo1[1] = new OKI6295();
                    OKI6295.oo1[1].okirom = Machine.GetRom("oki2.rom");
                    promsrom = new byte[0x10000]; // Machine.GetRom("proms.rom");
                    if (Memory.mainrom == null || Memory.audiorom == null || scrollrom[0] == null || scrollrom[1] == null || scrollrom[2] == null || spritesrom == null || OKI6295.oo1[0].okirom == null || OKI6295.oo1[1].okirom == null || promsrom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "avspirit"://B
                case "monkelf":
                case "edf":
                case "edfa":
                case "edfu":
                case "hayaosi1":
                    //case "edfbl":
                    Memory.mainram = new byte[0x20000];
                    Memory.audioram = new byte[0x10000];
                    Memory.mainrom = new byte[0x10000]; //Machine.GetRom("maincpu.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    scrollrom[0] = new byte[0x10000]; //Machine.GetRom("scroll0.rom");
                    scrollrom[1] = new byte[0x10000]; // Machine.GetRom("scroll1.rom");
                    scrollrom[2] = new byte[0x10000]; //Machine.GetRom("scroll2.rom");
                    spritesrom = new byte[0x10000]; //Machine.GetRom("sprites.rom");
                    OKI6295.oo1[0] = new OKI6295();
                    OKI6295.oo1[0].okirom = Machine.GetRom("oki1.rom");
                    OKI6295.oo1[1] = new OKI6295();
                    OKI6295.oo1[1].okirom = Machine.GetRom("oki2.rom");
                    promsrom = new byte[0x10000]; //Machine.GetRom("proms.rom");
                    if (Memory.mainrom == null || Memory.audiorom == null || scrollrom[0] == null || scrollrom[1] == null || scrollrom[2] == null || spritesrom == null || OKI6295.oo1[0].okirom == null || OKI6295.oo1[1].okirom == null || promsrom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "64street"://C
                case "64streetj":
                case "64streetja":
                case "bigstrik":
                case "chimerab":
                case "cybattlr":
                    Memory.mainram = new byte[0x10000];
                    Memory.audioram = new byte[0x10000];
                    Memory.mainrom = new byte[0x10000]; //Machine.GetRom("maincpu.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    scrollrom[0] = new byte[0x10000]; //Machine.GetRom("scroll0.rom");
                    scrollrom[1] = new byte[0x10000]; //Machine.GetRom("scroll1.rom");
                    scrollrom[2] = new byte[0x10000]; //Machine.GetRom("scroll2.rom");
                    spritesrom = new byte[0x10000]; //Machine.GetRom("sprites.rom");
                    OKI6295.oo1[0] = new OKI6295();
                    OKI6295.oo1[0].okirom = Machine.GetRom("oki1.rom");
                    OKI6295.oo1[1] = new OKI6295();
                    OKI6295.oo1[1].okirom = Machine.GetRom("oki2.rom");
                    promsrom = new byte[0x10000]; //Machine.GetRom("proms.rom");
                    if (Memory.mainrom == null || Memory.audiorom == null || scrollrom[0] == null || scrollrom[1] == null || scrollrom[2] == null || spritesrom == null || OKI6295.oo1[0].okirom == null || OKI6295.oo1[1].okirom == null || promsrom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "peekaboo"://D
                case "peakaboou":
                    Memory.mainram = new byte[0x10000];
                    Memory.audioram = new byte[0x10000];
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    scrollrom[0] = Machine.GetRom("scroll0.rom");
                    scrollrom[1] = Machine.GetRom("scroll1.rom");
                    spritesrom = Machine.GetRom("sprites.rom");
                    OKI6295.oo1[0] = new OKI6295();
                    OKI6295.oo1[0].okirom = Machine.GetRom("oki1.rom");
                    promsrom = Machine.GetRom("proms.rom");
                    if (Memory.mainrom == null || scrollrom[0] == null || scrollrom[1] == null || spritesrom == null || OKI6295.oo1[0].okirom == null || promsrom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
            }
            Drawgfx.spritecount = spritesrom.Length / 0x100;
            if (Machine.bRom)
            {
                switch (Machine.sName)
                {
                    case "lomakai":
                    case "makaiden":
                    case "kickoff":
                    case "kazan":
                    case "iganinju":
                        dsw1 = 0xbf;
                        dsw2 = 0xbf;
                        break;
                    case "p47":
                    case "p47j":
                    case "p47je":
                    case "hachoo":
                    case "edfp":
                    case "soldam":
                    case "soldamj":
                    case "edf":
                    case "edfa":
                    case "edfu":
                    //case "edfbl":
                    case "hayaosi1":
                    case "bigstrik":
                    case "peekaboo":
                    case "peakaboou":
                        dsw1 = 0xff;
                        dsw2 = 0xff;
                        break;
                    case "tshingen":
                    case "tshingena":
                        dsw1 = 0xff;
                        dsw2 = 0xdd;
                        break;                    
                    case "astyanax":
                    case "lordofk":
                    case "jitsupro":
                    case "rodland":
                    case "rodlandj":
                    case "rittam":
                    case "rodlandjb":
                        dsw1 = 0xbf;
                        dsw2 = 0xff;
                        break;
                    case "plusalph":
                    case "stdragon":
                    case "stdragona":
                    case "stdragonb":
                        dsw1 = 0xff;
                        dsw2 = 0xbf;
                        break;
                    case "phantasm":
                    case "avspirit":
                    case "monkelf":
                        dsw1 = 0xff;
                        dsw2 = 0xfd;
                        break;                    
                    case "64street":
                    case "64streetj":
                    case "64streetja":
                        dsw1 = 0xff;
                        dsw2 = 0xbd;
                        break;                    
                    case "chimerab":
                        dsw1 = 0xbd;
                        dsw2 = 0xff;
                        break;
                    case "cybattlr":
                        dsw1 = 0xff;
                        dsw2 = 0xbf;
                        break;
                }
            }
        }
        public static void machine_reset_megasys1()
        {
            megasys1_ignore_oki_status = 1;
            ip_latched = 0x0006;
        }
        public static void machine_reset_megasys1_hachoo()
        {
            megasys1_ignore_oki_status = 0;
        }
        public static ushort dsw1_r()
        {
            return dsw1;
        }
        public static ushort dsw2_r()
        {
            return dsw2;
        }
        public static ushort dsw_r()
        {
            return (ushort)((dsw1 << 8) | dsw2);
        }
        public static void megasys1A_scanline()
        {
            switch (Cpuexec.cpu[0].iloops)
            {
                case 240:
                    Cpuint.cpunum_set_input_line(0, 2, LineState.HOLD_LINE);
                    break;
                case 16:
                    Cpuint.cpunum_set_input_line(0, 1, LineState.HOLD_LINE);
                    break;
                case 128:
                    Cpuint.cpunum_set_input_line(0, 3, LineState.HOLD_LINE);
                    break;
            }
        }
        public static void megasys1B_scanline()
        {
            switch (Cpuexec.cpu[0].iloops)
            {
                case 240:
                    Cpuint.cpunum_set_input_line(0, 4, LineState.HOLD_LINE);
                    break;
                case 0:
                    Cpuint.cpunum_set_input_line(0, 2, LineState.HOLD_LINE);
                    break;
                case 128:
                    Cpuint.cpunum_set_input_line(0, 1, LineState.HOLD_LINE);
                    break;
            }
        }
        public static ushort ip_select_r()
        {
            return ip_latched;
        }
        public static void ip_select_w(ushort data)
        {
            int i;
            for (i = 0; i < 7; i++)
            {
                if ((data & 0x00ff) == ip_select_values[i])
                {
                    break;
                }
            }
            switch (i)
            {
                case 0:
                    ip_latched = (ushort)shorts;
                    break;
                case 1:
                    ip_latched = (ushort)short1;
                    break;
                case 2:
                    ip_latched = (ushort)short2;
                    break;
                case 3:
                    ip_latched = dsw1_r();
                    break;
                case 4:
                    ip_latched = dsw2_r();
                    break;
                case 5:
                    ip_latched = 0x0d;
                    break;
                case 6:
                    ip_latched = 0x06;
                    break;
                default:
                    return;
            }
            Cpuint.cpunum_set_input_line(0, 2, LineState.HOLD_LINE);
        }
        public static void ip_select_w1(byte data)
        {
            int i1 = 1;
        }
        public static void ip_select_w2(byte data)
        {
            int i1 = 1;
        }
        public static void interrupt_D()
        {
            Cpuint.cpunum_set_input_line(0, 2, LineState.HOLD_LINE);
        }
        public static void megasys1_sound_irq(int irq)
        {
            if (irq != 0)
            {
                Cpuint.cpunum_set_input_line(1, 4, LineState.HOLD_LINE);
            }
        }
        public static ushort oki_status_0_r()
        {
            if (megasys1_ignore_oki_status == 1)
            {
                return 0;
            }
            else
            {
                return (ushort)OKI6295.oo1[0].okim6295_status_r();
            }
        }
        public static ushort oki_status_1_r()
        {
            if (megasys1_ignore_oki_status == 1)
            {
                return 0;
            }
            else
            {
                return (ushort)OKI6295.oo1[1].okim6295_status_r();
            }
        }
        public static void irq_handler(int irq)
        {
            Cpuint.cpunum_set_input_line(1, 0, irq != 0 ? LineState.ASSERT_LINE : LineState.CLEAR_LINE);
        }
        public static void driver_init()
        {
            mainram_offset = 0;
            switch (Machine.sName)
            {
                case "avspirit":
                case "monkelf":
                    ip_select_values[0] = 0x37;
                    ip_select_values[1] = 0x35;
                    ip_select_values[2] = 0x36;
                    ip_select_values[3] = 0x33;
                    ip_select_values[4] = 0x34;
                    ip_select_values[5] = 0xff;
                    ip_select_values[6] = 0x06;
                    mainram_offset = 0x10000;
                    break;
                case "edf":
                case "edfa":
                case "edfu":
                    ip_select_values[0] = 0x20;
                    ip_select_values[1] = 0x21;
                    ip_select_values[2] = 0x22;
                    ip_select_values[3] = 0x23;
                    ip_select_values[4] = 0x24;
                    ip_select_values[5] = 0xf0;
                    ip_select_values[6] = 0x06;
                    break;
                case "hayaosi1":
                    ip_select_values[0] = 0x51;
                    ip_select_values[1] = 0x52;
                    ip_select_values[2] = 0x53;
                    ip_select_values[3] = 0x54;
                    ip_select_values[4] = 0x55;
                    ip_select_values[5] = 0xfc;
                    ip_select_values[6] = 0x06;
                    break;
                case "64street":
                case "64streetj":
                case "64streetja":
                    ip_select_values[0] = 0x57;
                    ip_select_values[1] = 0x53;
                    ip_select_values[2] = 0x54;
                    ip_select_values[3] = 0x55;
                    ip_select_values[4] = 0x56;
                    ip_select_values[5] = 0xfa;
                    ip_select_values[6] = 0x06;
                    break;
                case "bigstrik":
                    ip_select_values[0] = 0x58;
                    ip_select_values[1] = 0x54;
                    ip_select_values[2] = 0x55;
                    ip_select_values[3] = 0x56;
                    ip_select_values[4] = 0x57;
                    ip_select_values[5] = 0xfb;
                    ip_select_values[6] = 0x06;
                    break;
                case "chimerab":
                case "cybattlr":
                    ip_select_values[0] = 0x56;
                    ip_select_values[1] = 0x52;
                    ip_select_values[2] = 0x53;
                    ip_select_values[3] = 0x54;
                    ip_select_values[4] = 0x55;
                    ip_select_values[5] = 0xf2;
                    ip_select_values[6] = 0x06;
                    break;
            }
        }
        public static void play_megasys1_jitsupro(TrackInfo ti)
        {
            Inptport.lsFA.Add(new FrameArray(Video.screenstate.frame_number, 0, (byte)ti.TrackID));
            Inptport.lsFA.Add(new FrameArray(Video.screenstate.frame_number + 1, 0, 0x00));
        }
        public static void stop_megasys1_jitsupro(TrackInfo ti)
        {
            Inptport.lsFA.Add(new FrameArray(Video.screenstate.frame_number, 0, (byte)ti.TrackID));
            Inptport.lsFA.Add(new FrameArray(Video.screenstate.frame_number + 1, 0, 0x00));
        }
        public static void stopandplay_megasys1_jitsupro(TrackInfo ti)
        {
            Inptport.lsFA.Add(new FrameArray(Video.screenstate.frame_number, 0, (byte)RomInfo.IStop));
            Inptport.lsFA.Add(new FrameArray(Video.screenstate.frame_number + 1, 0, 0x00));
            Inptport.lsFA.Add(new FrameArray(Video.screenstate.frame_number + 2, 0, (byte)ti.TrackID));
            Inptport.lsFA.Add(new FrameArray(Video.screenstate.frame_number + 3, 0, 0x00));
        }
    }
}
