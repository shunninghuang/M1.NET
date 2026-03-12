using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mame
{
    public partial class Technos
    {
        public static byte dsw0, dsw1;
        public static byte[] ddragon_bgvideoram, ddragon_fgvideoram, ddragon_spriteram;
        public static ushort ddragon_scrollx_hi, ddragon_scrolly_hi;
        public static byte ddragon_scrollx_lo, ddragon_scrolly_lo, technos_video_hw;
        public static Timer.emu_timer scanline_timer;
        public static byte dd_sub_cpu_busy;
        public static byte sprite_irq, sound_irq, ym_irq, snd_cpu;
        public static uint[] adpcm_pos = new uint[2], adpcm_end = new uint[2];
        public static byte[] adpcm_idle = new byte[2];
        public static byte[] darktowr_mcu_ports = new byte[8];
        public static int[] adpcm_data = new int[2];
        public static int scanline_param;
        public static int darktowr_flag = 1;
        public static int basebankmain,gfxtotalelement;
        public static byte[] subrom, adpcmrom, mcurom;
        public static byte[] mainram2, subram, mcuram;
        public static byte[] gfx1rom, gfx2rom, gfx3rom;
        public static void DdragonInit()
        {
            int i, n;
            Machine.bRom = true;
            switch (Machine.sName)
            {
                case "ddragon":
                case "ddragonw":
                case "ddragonw1":
                case "ddragonu":
                case "ddragonua":
                case "ddragonub":
                case "ddragonb2":
                case "ddragonb":
                case "ddragonba":
                    Memory.mainrom = new byte[0x10000];// Machine.GetRom("maincpu.rom");
                    subrom = new byte[0x10000]; //Machine.GetRom("sub.rom");
                    Memory.audiorom = Machine.GetRom("soundcpu.rom");
                    gfx1rom = new byte[0x10000]; //Machine.GetRom("gfx1.rom");
                    gfx2rom = new byte[0x10000]; //Machine.GetRom("gfx2.rom");
                    gfx3rom = new byte[0x10000]; //Machine.GetRom("gfx3.rom");
                    adpcmrom = Machine.GetRom("adpcm.rom");
                    Memory.mainram = new byte[0x1000];
                    mainram2 = new byte[0x400];
                    subram = new byte[0x1000];
                    Memory.audioram = new byte[0x1000];
                    ddragon_bgvideoram = new byte[0x800];
                    ddragon_fgvideoram = new byte[0x800];
                    ddragon_spriteram = new byte[0x1000];
                    Generic.paletteram = new byte[0x200];
                    Generic.paletteram_2 = new byte[0x200];
                    if (Memory.mainrom == null || subrom == null || Memory.audiorom == null || gfx1rom == null || gfx2rom == null || gfx3rom == null || adpcmrom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "ddragon2":
                case "ddragon2u":
                case "ddragon2b":
                    Memory.mainrom = new byte[0x10000]; //Machine.GetRom("maincpu.rom");
                    subrom = new byte[0x10000]; //Machine.GetRom("sub.rom");
                    Memory.audiorom = Machine.GetRom("soundcpu.rom");
                    gfx1rom = new byte[0x10000]; //Machine.GetRom("gfx1.rom");
                    gfx2rom = new byte[0x10000]; //Machine.GetRom("gfx2.rom");
                    gfx3rom = new byte[0x10000]; //Machine.GetRom("gfx3.rom");
                    OKI6295.oo1[0] = new OKI6295();
                    OKI6295.oo1[0].okirom = Machine.GetRom("oki.rom");
                    Memory.mainram = new byte[0x1800];
                    subram = new byte[0x1000];
                    Memory.audioram = new byte[0x800];
                    ddragon_bgvideoram = new byte[0x800];
                    ddragon_fgvideoram = new byte[0x800];
                    ddragon_spriteram = new byte[0x1000];
                    Generic.paletteram = new byte[0x200];
                    Generic.paletteram_2 = new byte[0x200];
                    if (Memory.mainrom == null || subrom == null || Memory.audiorom == null || gfx1rom == null || gfx2rom == null || gfx3rom == null || OKI6295.oo1[0].okirom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "tstrike":
                case "tstrikea":
                case "ddungeon":
                case "ddungeone":
                case "darktowr":
                    Memory.mainrom = new byte[0x10000]; //Machine.GetRom("maincpu.rom");
                    subrom = new byte[0x10000]; //Machine.GetRom("sub.rom");
                    Memory.audiorom = Machine.GetRom("soundcpu.rom");
                    mcurom = new byte[0x10000]; //Machine.GetRom("mcu.rom");
                    gfx1rom = new byte[0x10000]; //Machine.GetRom("gfx1.rom");
                    gfx2rom = new byte[0x10000]; //Machine.GetRom("gfx2.rom");
                    gfx3rom = new byte[0x10000]; //Machine.GetRom("gfx3.rom");
                    adpcmrom = Machine.GetRom("adpcm.rom");
                    Memory.mainram = new byte[0x1000];
                    mainram2 = new byte[0x400];
                    subram = new byte[0x1000];
                    Memory.audioram = new byte[0x1000];
                    mcuram = new byte[0x80];
                    ddragon_bgvideoram = new byte[0x800];
                    ddragon_fgvideoram = new byte[0x800];
                    ddragon_spriteram = new byte[0x1000];
                    Generic.paletteram = new byte[0x200];
                    Generic.paletteram_2 = new byte[0x200];
                    if (Memory.mainrom == null || subrom == null || Memory.audiorom == null || mcurom == null || gfx1rom == null || gfx2rom == null || gfx3rom == null || adpcmrom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "toffy":
                case "stoffy":
                case "stoffyu":
                    Memory.mainrom = new byte[0x10000]; //Machine.GetRom("maincpu.rom");
                    Memory.audiorom = Machine.GetRom("soundcpu.rom");
                    gfx1rom = new byte[0x10000]; //Machine.GetRom("gfx1.rom");
                    gfx2rom = new byte[0x10000]; //Machine.GetRom("gfx2.rom");
                    gfx3rom = new byte[0x10000]; //Machine.GetRom("gfx3.rom");
                    Memory.mainram = new byte[0x1000];
                    mainram2 = new byte[0x400];
                    subram = new byte[0x1000];
                    Memory.audioram = new byte[0x1000];
                    ddragon_bgvideoram = new byte[0x800];
                    ddragon_fgvideoram = new byte[0x800];
                    ddragon_spriteram = new byte[0x1000];
                    Generic.paletteram = new byte[0x200];
                    Generic.paletteram_2 = new byte[0x200];
                    if (Memory.mainrom == null || Memory.audiorom == null || gfx1rom == null || gfx2rom == null || gfx3rom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
            }
            if (Machine.bRom)
            {
                switch (Machine.sName)
                {
                    case "ddragon":
                    case "ddragonw":
                    case "ddragonw1":
                    case "ddragonu":
                    case "ddragonua":
                    case "ddragonub":
                    case "ddragonb2":
                    case "ddragonb":
                    case "ddragonba":
                    case "ddragon2":
                    case "ddragon2u":
                    case "ddragon2b":
                    case "tstrike":
                    case "tstrikea":
                        dsw0 = 0xff;
                        dsw1 = 0xff;
                        break;
                    case "ddungeon":
                    case "ddungeone":
                        dsw0 = 0x00;
                        dsw1 = 0x9d;
                        break;
                    case "darktowr":
                        dsw0 = 0x00;
                        dsw1 = 0xff;
                        break;
                    case "toffy":
                    case "stoffy":
                    case "stoffyu":
                        dsw0 = 0x00;
                        dsw1 = 0x89;
                        break;
                }
            }
        }
        private static int scanline_to_vcount(int scanline)
        {
            int vcount = scanline + 8;
            if (vcount < 0x100)
            {
                return vcount;
            }
            else
            {
                return (vcount - 0x18) | 0x100;
            }
        }
        public static void ddragon_scanline_callback()
        {
            int scanline = scanline_param;
            int screen_height = Video.screenstate.height;
            int vcount_old = scanline_to_vcount((scanline == 0) ? screen_height - 1 : scanline - 1);
            int vcount = scanline_to_vcount(scanline);
            if (scanline > 0)
            {
                Video.video_screen_update_partial(scanline - 1);
            }
            if (vcount == 0xf8)
            {
                Cpuint.cpunum_set_input_line(0, (int)LineState.INPUT_LINE_NMI, LineState.ASSERT_LINE);
            }
            if ((vcount_old & 8) == 0 && (vcount & 8) != 0)
            {
                Cpuint.cpunum_set_input_line(0, 1, LineState.ASSERT_LINE);
            }
            if (++scanline >= screen_height)
            {
                scanline = 0;
            }
            scanline_param = scanline;
            Timer.timer_adjust_periodic(scanline_timer, Video.video_screen_get_time_until_pos(scanline, 0), Attotime.ATTOTIME_NEVER);
        }
        public static void machine_start_ddragon()
        {
            scanline_timer = Timer.timer_alloc_common(ddragon_scanline_callback, "ddragon_scanline_callback", false);
        }
        public static void machine_reset_ddragon()
        {
            dd_sub_cpu_busy = 1;
            adpcm_idle[0] = adpcm_idle[1] = 1;
            adpcm_data[0] = adpcm_data[1] = -1;
            Timer.timer_adjust_periodic(scanline_timer, Video.video_screen_get_time_until_pos(0, 0), Attotime.ATTOTIME_NEVER);
        }
        public static void ddragon_bankswitch_w(byte data)
        {
            ddragon_scrollx_hi = (ushort)((data & 0x01) << 8);
            ddragon_scrolly_hi = (ushort)((data & 0x02) << 7);
            Generic.flip_screen_set(~data & 0x04);
            if ((data & 0x10) != 0)
            {
                dd_sub_cpu_busy = 0;
            }
            else if (dd_sub_cpu_busy == 0)
            {
                Cpuint.cpunum_set_input_line(1, sprite_irq, (sprite_irq == (byte)LineState.INPUT_LINE_NMI) ? LineState.PULSE_LINE : LineState.HOLD_LINE);
            }
            basebankmain = 0x10000 + 0x4000 * ((data & 0xe0) >> 5);
        }
        public static void toffy_bankswitch_w(byte data)
        {
            ddragon_scrollx_hi = (ushort)((data & 0x01) << 8);
            ddragon_scrolly_hi = (ushort)((data & 0x02) << 7);
            basebankmain = 0x10000 + 0x4000 * ((data & 0x20) >> 5);
        }
        public static byte darktowr_mcu_bank_r(int offset)
        {
            if (Machine.sName =="tstrike")
            {
                if (cpu.hd6309.Hd6309.mm1[0].pc.LowWord == 0x9ace)
                {
                    return 0;
                }
                if (cpu.hd6309.Hd6309.mm1[0].pc.LowWord == 0x9ae4)
                {
                    return 0x63;
                }
                return Memory.mainram[0xbe1];
            }
            if (offset == 0x1401 || offset == 1)
            {
                return darktowr_mcu_ports[0];
            }
            return 0xff;
        }
        public static void darktowr_mcu_bank_w(int offset, byte data)
        {
            if (offset == 0x1400 || offset == 0)
            {
                darktowr_mcu_ports[1] = (byte)Neogeo.BITSWAP8((int)data, 0, 1, 2, 3, 4, 5, 6, 7);
            }
        }
        public static void darktowr_bankswitch_w(byte data)
        {
            int oldbank = (basebankmain - 0x10000) / 0x4000;
            int newbank = (data & 0xe0) >> 5;
            ddragon_scrollx_hi = (ushort)((data & 0x01) << 8);
            ddragon_scrolly_hi = (ushort)((data & 0x02) << 7);
            if ((data & 0x10) != 0)
            {
                dd_sub_cpu_busy = 0;
            }
            else if (dd_sub_cpu_busy == 0)
            {
                Cpuint.cpunum_set_input_line(1, sprite_irq, (sprite_irq == (byte)LineState.INPUT_LINE_NMI) ? LineState.PULSE_LINE : LineState.HOLD_LINE);
            }
            basebankmain = 0x10000 + 0x4000 * newbank;
            if (newbank == 4 && oldbank != 4)
            {
                darktowr_flag = 2;
            }
            else if (newbank != 4 && oldbank == 4)
            {
                darktowr_flag = 1;
            }
        }
        public static void ddragon_interrupt_w(int offset, byte data)
        {
            switch (offset)
            {
                case 0: /* 380b - NMI ack */
                    Cpuint.cpunum_set_input_line(0, (int)LineState.INPUT_LINE_NMI, LineState.CLEAR_LINE);
                    break;

                case 1: /* 380c - FIRQ ack */
                    Cpuint.cpunum_set_input_line(0, 1, LineState.CLEAR_LINE);
                    break;

                case 2: /* 380d - IRQ ack */
                    Cpuint.cpunum_set_input_line(0, 0, LineState.CLEAR_LINE);
                    break;

                case 3: /* 380e - SND irq */
                    Sound.soundlatch_w(data);
                    Cpuint.cpunum_set_input_line(snd_cpu, sound_irq, (sound_irq == (byte)LineState.INPUT_LINE_NMI) ? LineState.PULSE_LINE : LineState.HOLD_LINE);
                    break;

                case 4: /* 380f - ? */
                    /* Not sure what this is - almost certainly related to the sprite mcu */
                    break;
            }
        }
        public static void ddragon2_sub_irq_ack_w()
        {
            Cpuint.cpunum_set_input_line(1, sprite_irq, LineState.CLEAR_LINE);
        }
        public static void ddragon2_sub_irq_w()
        {
            Cpuint.cpunum_set_input_line(0, 0, LineState.ASSERT_LINE);
        }
        public static void irq_handler(int irq)
        {
            Cpuint.cpunum_set_input_line(snd_cpu, ym_irq, irq != 0 ? LineState.ASSERT_LINE : LineState.CLEAR_LINE);
        }
        public static byte sub_cpu_busy()
        {
            return dd_sub_cpu_busy;
        }
        public static void darktowr_mcu_w(ushort offset, byte data)
        {
            darktowr_mcu_ports[offset] = data;
        }
        public static byte getbytee()
        {
            byte result;
            if (Video.video_screen_get_vblank())
            {
                result = (byte)(bytee | 0x08);
            }
            else
            {
                result = bytee;
            }
            if (sub_cpu_busy() != 0)
            {
                result = (byte)(result | 0x10);
            }
            return result;
        }
        public static byte ddragon_hd63701_internal_registers_r()
        {
            return 0;
        }
        public static void ddragon_hd63701_internal_registers_w(int offset, byte data)
        {
            if (offset == 0x17)
            {
                if ((data & 3) != 0)
                {
                    Cpuint.cpunum_set_input_line(0, 0, LineState.ASSERT_LINE);
                    Cpuint.cpunum_set_input_line(1, sprite_irq, LineState.CLEAR_LINE);
                }
            }
        }
        public static byte ddragon_spriteram_r(int offset)
        {
            if (offset == 0x49 && cpu.hd6309.Hd6309.mm1[0].pc.LowWord == 0x6261 && ddragon_spriteram[offset] == 0x1f)
            {
                return 0x1;
            }
            return ddragon_spriteram[offset];
        }
        public static void ddragon_spriteram_w(int offset, byte data)
        {
            if (Cpuexec.activecpu == 1 && offset == 0)
            {
                dd_sub_cpu_busy = 1;
            }
            ddragon_spriteram[offset] = data;
        }
        public static void dd_adpcm_w(int offset, byte data)
        {
            int chip = offset & 1;
            switch (offset / 2)
            {
                case 3:
                    adpcm_idle[chip] = 1;
                    MSM5205.msm5205_reset_w(chip, 1);
                    break;
                case 2:
                    adpcm_pos[chip] = (uint)((data & 0x7f) * 0x200);
                    break;
                case 1:
                    adpcm_end[chip] = (uint)((data & 0x7f) * 0x200);
                    break;
                case 0:
                    adpcm_idle[chip] = 0;
                    MSM5205.msm5205_reset_w(chip, 0);
                    break;
            }
        }
        public static void dd_adpcm_int(int chip)
        {
            if (adpcm_pos[chip] >= adpcm_end[chip] || adpcm_pos[chip] >= 0x10000)
            {
                adpcm_idle[chip] = 1;
                MSM5205.msm5205_reset_w(chip, 1);
            }
            else if (adpcm_data[chip] != -1)
            {
                MSM5205.msm5205_data_w(chip, adpcm_data[chip] & 0x0f);
                adpcm_data[chip] = -1;
            }
            else
            {
                int rom_offset = 0x10000 * chip;
                adpcm_data[chip] = adpcmrom[rom_offset + adpcm_pos[chip]++];
                MSM5205.msm5205_data_w(chip, adpcm_data[chip] >> 4);
            }
        }
        public static byte dd_adpcm_status_r()
        {
            return (byte)(adpcm_idle[0] + (adpcm_idle[1] << 1));
        }
        public static void ddragnba_port_w()
        {
            Cpuint.cpunum_set_input_line(0, 0, LineState.ASSERT_LINE);
            Cpuint.cpunum_set_input_line(1, sprite_irq, LineState.CLEAR_LINE);
        }
        public static void driver_init()
        {
            switch (Machine.sName)
            {
                case "ddragon":
                case "ddragonw":
                case "ddragonw1":
                case "ddragonu":
                case "ddragonua":
                case "ddragonub":
                case "ddragonb2":
                case "ddragonb":
                case "ddragonba":
                    driver_init_ddragon();
                    break;
                case "ddragon2":
                case "ddragon2u":
                case "ddragon2b":
                    driver_init_ddragon2();
                    break;
                case "tstrike":
                case "tstrikea":
                case "ddungeon":
                case "ddungeone":
                case "darktowr":
                    driver_init_darktowr();
                    break;
                case "toffy":
                case "stoffy":
                case "stoffyu":
                    driver_init_toffy();
                    break;
            }
        }
        public static void driver_init_ddragon()
        {
            snd_cpu = 2;
            sprite_irq = 32;
            sound_irq = 0;
            ym_irq = 1;
            technos_video_hw = 0;
        }
        public static void driver_init_ddragon2()
        {
            snd_cpu = 2;
            sprite_irq = 32;
            sound_irq = 32;
            ym_irq = 0;
            technos_video_hw = 2;
        }
        public static void driver_init_darktowr()
        {
            snd_cpu = 2;
            sprite_irq = 32;
            sound_irq = 0;
            ym_irq = 1;
            technos_video_hw = 0;
        }
        public static void driver_init_toffy()
        {
            snd_cpu = 1;
            sound_irq = 0;
            ym_irq = 1;
            technos_video_hw = 0;
        }
        public static void play_technos_default(TrackInfo ti)
        {
            Inptport.lsFA.Add(new FrameArray(Video.screenstate.frame_number, 0, (byte)ti.TrackID));
        }
        public static void stop_technos_default(TrackInfo ti)
        {
            Inptport.lsFA.Add(new FrameArray(Video.screenstate.frame_number, 0, (byte)ti.TrackID));
        }
        public static void stopandplay_technos_default(TrackInfo ti)
        {
            Inptport.lsFA.Add(new FrameArray(Video.screenstate.frame_number, 0, (byte)RomInfo.IStop));
            Inptport.lsFA.Add(new FrameArray(Video.screenstate.frame_number + 1, 0, (byte)ti.TrackID));
        }
    }
}
