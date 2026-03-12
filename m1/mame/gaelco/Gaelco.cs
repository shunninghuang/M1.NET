using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Gaelco
    {
        public static byte[] gfx1rom, gfx2rom, okirom1;
        public static byte dsw1, dsw2, bytes;
        public static void GaelcoInit()
        {
            int i, n;
            Machine.bRom = true;
            Memory.mainrom = new byte[0x10000];// Machine.GetRom("maincpu.rom");
            Memory.audiorom = Machine.GetRom("audiocpu.rom");
            gfx1rom = new byte[0x10000]; Machine.GetRom("gfx1.rom");
            gfx2rom = new byte[0x10000]; Machine.GetRom("gfx2.rom");
            okirom1 = Machine.GetRom("oki.rom");
            OKI6295.oo1[0] = new OKI6295();
            OKI6295.oo1[0].okirom = Machine.GetRom("oki.rom");
            Memory.mainram = new byte[0x10000];
            Memory.audioram = new byte[0x800];
            gaelco_vregs = new ushort[4];
            gaelco_videoram = new ushort[0x1000];
            gaelco_spriteram = new ushort[0x800];
            gaelco_screen = new ushort[0x1000];
            Generic.paletteram16 = new ushort[0x400];
            if (Memory.mainrom == null || gfx1rom == null || gfx2rom == null || OKI6295.oo1[0].okirom == null)
            {
                Machine.bRom = false;
            }
            if (Machine.bRom)
            {
                switch (Machine.sName)
                {
                    case "bigkarnk":
                        dsw1 = 0xff;
                        dsw2 = 0xce;
                        bytes = 0xff;
                        break;
                    case "biomtoy":
                    case "biomtoya":
                    case "biomtoyb":
                    case "biomtoyc":
                    case "bioplayc":
                        dsw1 = 0xff;
                        dsw2 = 0xfb;
                        break;
                    case "maniacsp":
                        dsw1 = 0xff;
                        dsw2 = 0xf5;
                        break;
                    case "lastkm":
                        dsw1 = 0xff;
                        dsw2 = 0xff;
                        break;
                    case "squash":
                        dsw1 = 0xff;
                        dsw2 = 0xdf;
                        break;
                    case "thoop":
                        dsw1 = 0xff;
                        dsw2 = 0xcf;
                        break;
                }
            }
        }
        public static void bigkarnk_sound_command_w(ushort data)
        {
            Sound.soundlatch_w((ushort)(data & 0xff));
            Cpuint.cpunum_set_input_line(1, 1, LineState.HOLD_LINE);
        }
        public static void bigkarnk_sound_command_w2(byte data)
        {
            Sound.soundlatch_w(data);
            Cpuint.cpunum_set_input_line(1, 1, LineState.HOLD_LINE);
        }
        public static void bigkarnk_coin_w(int offset, ushort data)
        {
            switch ((offset >> 3))
            {
                case 0x00:	/* Coin Lockouts */
                case 0x01:
                    Generic.coin_lockout_w((offset >> 3) & 0x01, ~data & 0x01);
                    break;
                case 0x02:	/* Coin Counters */
                case 0x03:
                    Generic.coin_counter_w((offset >> 3) & 0x01, data & 0x01);
                    break;
            }
        }
        public static void bigkarnk_coin_w2(int offset, byte data)
        {
            switch ((offset >> 3))
            {
                case 0x00:	/* Coin Lockouts */
                case 0x01:
                    Generic.coin_lockout_w((offset >> 3) & 0x01, ~data & 0x01);
                    break;
                case 0x02:	/* Coin Counters */
                case 0x03:
                    Generic.coin_counter_w((offset >> 3) & 0x01, data & 0x01);
                    break;
            }
        }
        public static void OKIM6295_bankswitch_w(ushort data)
        {
            Array.Copy(okirom1, (data & 0x0f) * 0x10000, OKI6295.oo1[0].okirom, 0x30000, 0x10000);
        }
        public static void OKIM6295_bankswitch_w2(byte data)
        {
            Array.Copy(okirom1, (data & 0x0f) * 0x10000, OKI6295.oo1[0].okirom, 0x30000, 0x10000);
        }
        public static void irqack_w()
        {
            Cpuint.cpunum_set_input_line(0, 6, LineState.CLEAR_LINE);
        }
        public static void gaelco_vram_encrypted_w(int offset, ushort data)
        {
            data = gaelco_decrypt(offset, data, 0x0f, 0x4228);
            gaelco_videoram[offset] = data;
            gaelco_tilemap[offset >> 11].tilemap_mark_tile_dirty(((offset << 1) & 0x0fff) >> 2);
        }
        public static void gaelco_vram_encrypted_w1(int offset, byte data)
        {
            data = (byte)gaelco_decrypt(offset, data, 0x0f, 0x4228);
            gaelco_videoram[offset] = (ushort)((data << 8) | (gaelco_videoram[offset] & 0xff));
            gaelco_tilemap[offset >> 11].tilemap_mark_tile_dirty(((offset << 1) & 0x0fff) >> 2);
        }
        public static void gaelco_vram_encrypted_w2(int offset, byte data)
        {
            data = (byte)gaelco_decrypt(offset, data, 0x0f, 0x4228);
            gaelco_videoram[offset] = (ushort)((gaelco_videoram[offset] & 0xff00) | data);
            gaelco_tilemap[offset >> 11].tilemap_mark_tile_dirty(((offset << 1) & 0x0fff) >> 2);
        }
        public static void gaelco_encrypted_w(int offset, ushort data)
        {
            data = gaelco_decrypt(offset, data, 0x0f, 0x4228);
            gaelco_screen[offset] = data;
        }
        public static void gaelco_encrypted_w1(int offset, byte data)
        {
            data = (byte)gaelco_decrypt(offset, data, 0x0f, 0x4228);
            gaelco_screen[offset] = (ushort)((data << 8) | (gaelco_screen[offset] & 0xff));
        }
        public static void gaelco_encrypted_w2(int offset, byte data)
        {
            data = (byte)gaelco_decrypt(offset, data, 0x0f, 0x4228);
            gaelco_screen[offset] = (ushort)((gaelco_screen[offset] & 0xff00) | data);
        }
        public static void thoop_vram_encrypted_w(int offset,ushort data)
        {
            data = gaelco_decrypt(offset, data, 0x0e, 0x4228);
            gaelco_videoram[offset] = data;
            gaelco_tilemap[offset >> 11].tilemap_mark_tile_dirty(((offset << 1) & 0x0fff) >> 2);
        }
        public static void thoop_vram_encrypted_w1(int offset, byte data)
        {
            data = (byte)gaelco_decrypt(offset, data, 0x0e, 0x4228);
            gaelco_videoram[offset] = (ushort)((data << 8) | (gaelco_videoram[offset] & 0xff));
            gaelco_tilemap[offset >> 11].tilemap_mark_tile_dirty(((offset << 1) & 0x0fff) >> 2);
        }
        public static void thoop_vram_encrypted_w2(int offset, byte data)
        {
            data = (byte)gaelco_decrypt(offset, data, 0x0e, 0x4228);
            gaelco_videoram[offset] = (ushort)((gaelco_videoram[offset] & 0xff00) | data);
            gaelco_tilemap[offset >> 11].tilemap_mark_tile_dirty(((offset << 1) & 0x0fff) >> 2);
        }
        public static void thoop_encrypted_w(int offset, ushort data)
        {
            data = gaelco_decrypt(offset, data, 0x0e, 0x4228);
            gaelco_screen[offset] = data;
        }
        public static void thoop_encrypted_w1(int offset, byte data)
        {
            data = (byte)gaelco_decrypt(offset, data, 0x0e, 0x4228);
            gaelco_screen[offset] = (ushort)((data << 8) | (gaelco_screen[offset] & 0xff));
        }
        public static void thoop_encrypted_w2(int offset, byte data)
        {
            data = (byte)gaelco_decrypt(offset, data, 0x0e, 0x4228);
            gaelco_screen[offset] = (ushort)((gaelco_screen[offset] & 0xff00) | data);
        }
        public static void machine_reset_gaelco()
        {

        }
    }
}
