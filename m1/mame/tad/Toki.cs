using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Tad
    {
        public static byte[] audioromop, gfx1rom, gfx2rom, gfx3rom, gfx4rom;
        public static int toggle, msm5205next, basebanksnd;
        public static ushort dsw0;
        public static void TokiInit()
        {
            Machine.bRom = true;
            switch (Machine.sName)
            {
                case "toki":
                case "tokiu":
                case "tokip":
                case "tokia":
                case "tokiua":
                case "juju":
                case "jujuba":
                    Memory.mainram = new byte[0xd800];
                    Memory.audioram = new byte[0x800];
                    toki_background1_videoram16 = new ushort[0x400];
                    toki_background2_videoram16 = new ushort[0x400];
                    toki_scrollram16 = new ushort[0x30];
                    Generic.paletteram16 = new ushort[0x400];
                    Generic.videoram16 = new ushort[0x400];
                    Generic.spriteram16 = new ushort[0x400];
                    Generic.buffered_spriteram16 = new ushort[0x400];
                    Memory.mainrom = new byte[0x10000];//Machine.GetRom("maincpu.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    audioromop = Machine.GetRom("audiocpuop.rom");
                    gfx1rom = new byte[0x10000];//Machine.GetRom("gfx1.rom");
                    gfx2rom = new byte[0x10000];//Machine.GetRom("gfx2.rom");
                    gfx3rom = new byte[0x10000];//Machine.GetRom("gfx3.rom");
                    gfx4rom = new byte[0x10000];//Machine.GetRom("gfx4.rom");
                    OKI6295.oo1[0] = new OKI6295();
                    OKI6295.oo1[0].okirom = Machine.GetRom("oki.rom");
                    if (Memory.mainrom == null || Memory.audiorom == null || audioromop == null || gfx1rom == null || gfx2rom == null || gfx3rom == null || gfx4rom == null || OKI6295.oo1[0].okirom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "tokib":
                case "jujub":
                    Memory.mainram = new byte[0xe000];
                    Memory.audioram = new byte[0x800];
                    toki_background1_videoram16 = new ushort[0x400];
                    toki_background2_videoram16 = new ushort[0x400];
                    toki_scrollram16 = new ushort[0x30];
                    Generic.paletteram16 = new ushort[0x400];
                    Generic.videoram16 = new ushort[0x400];
                    Generic.spriteram16 = new ushort[0x400];
                    Generic.buffered_spriteram16 = new ushort[0x400];
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    gfx1rom = Machine.GetRom("gfx1.rom");
                    gfx2rom = Machine.GetRom("gfx2.rom");
                    gfx3rom = Machine.GetRom("gfx3.rom");
                    gfx4rom = Machine.GetRom("gfx4.rom");
                    if (Memory.mainrom == null || Memory.audiorom == null || gfx1rom == null || gfx2rom == null || gfx3rom == null || gfx4rom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
            }
            if (Machine.bRom)
            {
                dsw0 = 0xffdf;
            }
        }
        public static void tokib_soundcommand16_w(ushort data)
        {
            Sound.soundlatch_w((ushort)(data & 0xff));
            Cpuint.cpunum_set_input_line(1, 0, LineState.HOLD_LINE);
        }
        public static void tokib_soundcommand16_w1(byte data)
        {
            Sound.soundlatch_w((ushort)data);
            Cpuint.cpunum_set_input_line(1, 0, LineState.HOLD_LINE);
        }
        public static ushort pip16_r()
        {
            return 0xffff;
        }
        public static void toki_adpcm_int(int data)
        {
            MSM5205.msm5205_data_w(0, msm5205next);
            msm5205next >>= 4;
            toggle ^= 1;
            if (toggle != 0)
            {
                Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_NMI, LineState.PULSE_LINE);
            }
        }
        public static void toki_adpcm_control_w(byte data)
        {
            basebanksnd = 0x10000 + (data & 0x01) * 0x4000;
            MSM5205.msm5205_reset_w(0, data & 0x08);
        }
        public static void toki_adpcm_data_w(byte data)
        {
            msm5205next = data;
        }
    }
}
