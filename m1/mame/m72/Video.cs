using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class M72
    {
        public static byte[] m72_videoram1,m72_videoram2;
        public static ushort[] majtitle_rowscrollram;
        public static int m72_raster_irq_position;
        public static ushort[] m72_spriteram;
        private static ushort[] uuB200;
        public static int scrollx1, scrolly1, scrollx2, scrolly2;
        public static int video_off, spriteram_size, majtitle_rowscroll;
        public static void video_start_m72()
        {
            int i;
            bg_tilemap = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 8, 8, 64, 64);
            fg_tilemap = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 8, 8, 64, 64);

            bg_tilemap.total_elements = M72.gfx21rom.Length / 0x40;
            bg_tilemap.pen_to_flags = new byte[3, 16];
            for (i = 0; i < 16; i++)
            {
                bg_tilemap.pen_to_flags[0, i] = 0x20;
            }
            for (i = 0; i < 8; i++)
            {
                bg_tilemap.pen_to_flags[1, i] = 0x20;
            }
            for (i = 8; i < 16; i++)
            {
                bg_tilemap.pen_to_flags[1, i] = 0x10;
            }
            bg_tilemap.pen_to_flags[2, 0] = 0x20;
            for (i = 1; i < 16; i++)
            {
                bg_tilemap.pen_to_flags[2, i] = 0x10;
            }
            bg_tilemap.tilemap_draw_instance3 = bg_tilemap.tilemap_draw_instance_capcom_sf;
            fg_tilemap.total_elements = M72.gfx21rom.Length / 0x40;
            fg_tilemap.pen_to_flags = new byte[3, 32];
            fg_tilemap.pen_to_flags[0, 0] = 0;
            for (i = 1; i < 16; i++)
            {
                fg_tilemap.pen_to_flags[0, i] = 0x20;
            }
            fg_tilemap.pen_to_flags[1, 0] = 0;
            for (i = 1; i < 8; i++)
            {
                fg_tilemap.pen_to_flags[1, i] = 0x20;
            }
            for (i = 8; i < 16; i++)
            {
                fg_tilemap.pen_to_flags[1, i] = 0x10;
            }
            fg_tilemap.pen_to_flags[2, 0] = 0;
            for (i = 1; i < 16; i++)
            {
                fg_tilemap.pen_to_flags[2, i] = 0x10;
            }
            fg_tilemap.tilemap_draw_instance3 = fg_tilemap.tilemap_draw_instance_capcom_sf;
            switch (Machine.sName)
            {
                case "airduel":
                case "airduelm72":
                    bg_tilemap.tile_update3 = bg_tilemap.tile_update_m72_bg;
                    fg_tilemap.tile_update3 = fg_tilemap.tile_update_m72_fg;
                    break;
                case "ltswords":
                case "kengo":
                case "kengoa":
                    bg_tilemap.tile_update3 = bg_tilemap.tile_update_m72_bg_rtype2;
                    fg_tilemap.tile_update3 = fg_tilemap.tile_update_m72_fg_rtype2;
                    break;
            }
            Tilemap.lsTmap = new List<Tmap>();
            Tilemap.lsTmap.Add(bg_tilemap);
            Tilemap.lsTmap.Add(fg_tilemap);


            uuB200 = new ushort[0x200 * 0x200];
            Video.new_clip = new RECT();
            spriteram_size = 0x400;
            for (i = 0; i < 0x40000; i++)
            {
                uuB200[i] = 0x200;
            }
            m72_spriteram = new ushort[0x200];
            m72_videoram1 = new byte[0x4000];
            m72_videoram2 = new byte[0x4000];
            fg_tilemap.tilemap_set_scrolldx(0, 0);
            fg_tilemap.tilemap_set_scrolldy(-128, 16);
            bg_tilemap.tilemap_set_scrolldx(0, 0);
            bg_tilemap.tilemap_set_scrolldy(-128, 16);
            switch (Machine.sName)
            {
                case "ltswords":
                case "kengo":
                case "kengoa":
                    fg_tilemap.tilemap_set_scrolldx(6, 0);
                    bg_tilemap.tilemap_set_scrolldx(6, 0);
                    break;
            }
        }
        public static int m82_scan_rows(int col, int row, int num_cols, int num_rows)
        {
            return row * 256 + col;
        }
        public static void video_start_m82()
        {
            int i;
            fg_tilemap = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 8, 8, 64, 64);
            bg_tilemap = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 8, 8, 64, 64);
            bg_tilemap_large = Tmap.tilemap_create(m82_scan_rows, 8, 8, 128, 64);
            bg_tilemap.total_elements = M72.gfx21rom.Length / 0x40;
            bg_tilemap.pen_data = new byte[0x40];
            bg_tilemap.pen_to_flags = new byte[3, 16];
            for (i = 0; i < 16; i++)
            {
                bg_tilemap.pen_to_flags[0, i] = 0x20;
            }
            for (i = 0; i < 8; i++)
            {
                bg_tilemap.pen_to_flags[1, i] = 0x20;
            }
            for (i = 8; i < 16; i++)
            {
                bg_tilemap.pen_to_flags[1, i] = 0x10;
            }
            bg_tilemap.pen_to_flags[2, 0] = 0x20;
            for (i = 1; i < 16; i++)
            {
                bg_tilemap.pen_to_flags[2, i] = 0x10;
            }
            bg_tilemap.tilemap_draw_instance3 = bg_tilemap.tilemap_draw_instance_capcom_sf;
            fg_tilemap.total_elements = M72.gfx21rom.Length / 0x40;
            fg_tilemap.pen_to_flags = new byte[3, 32];
            fg_tilemap.pen_to_flags[0, 0] = 0;
            for (i = 1; i < 16; i++)
            {
                fg_tilemap.pen_to_flags[0, i] = 0x20;
            }
            fg_tilemap.pen_to_flags[1, 0] = 0;
            for (i = 1; i < 8; i++)
            {
                fg_tilemap.pen_to_flags[1, i] = 0x20;
            }
            for (i = 8; i < 16; i++)
            {
                fg_tilemap.pen_to_flags[1, i] = 0x10;
            }
            fg_tilemap.pen_to_flags[2, 0] = 0;
            for (i = 1; i < 16; i++)
            {
                fg_tilemap.pen_to_flags[2, i] = 0x10;
            }
            fg_tilemap.tilemap_draw_instance3 = fg_tilemap.tilemap_draw_instance_capcom_sf;
            bg_tilemap_large.total_elements = M72.gfx21rom.Length / 0x40;
            bg_tilemap_large.pen_to_flags = new byte[3, 16];
            for (i = 0; i < 16; i++)
            {
                bg_tilemap_large.pen_to_flags[0, i] = 0x20;
            }
            for (i = 0; i < 8; i++)
            {
                bg_tilemap_large.pen_to_flags[1, i] = 0x20;
            }
            for (i = 8; i < 16; i++)
            {
                bg_tilemap_large.pen_to_flags[1, i] = 0x10;
            }
            bg_tilemap_large.pen_to_flags[2, 0] = 0x20;
            for (i = 1; i < 16; i++)
            {
                bg_tilemap_large.pen_to_flags[2, i] = 0x10;
            }
            bg_tilemap_large.tilemap_draw_instance3 = bg_tilemap_large.tilemap_draw_instance_capcom_sf;
            bg_tilemap.tile_update3 = bg_tilemap.tile_update_m72_bg;
            fg_tilemap.tile_update3 = fg_tilemap.tile_update_m72_fg;
            bg_tilemap_large.tile_update3 = bg_tilemap_large.tile_update_m72_bg;
            Tilemap.lsTmap = new List<Tmap>();
            Tilemap.lsTmap.Add(bg_tilemap);
            Tilemap.lsTmap.Add(fg_tilemap);
            Tilemap.lsTmap.Add(bg_tilemap_large);

            uuB200 = new ushort[0x400 * 0x200];
            Video.new_clip = new RECT();
            spriteram_size = 0x400;
            for (i = 0; i < 0x80000; i++)
            {
                uuB200[i] = 0x200;
            }
        }
        public static ushort m72_palette1_r(int offset)
        {
            offset &= ~0x100;
            return (ushort)(Generic.paletteram16[offset] | 0xffe0);
        }
        public static ushort m72_palette2_r(int offset)
        {
            offset &= ~0x100;
            return (ushort)(Generic.paletteram16_2[offset] | 0xffe0);
        }
        public static void changecolor(int color, int r, int g, int b)
        {
            Palette.palette_entry_set_color1(color, Palette.make_rgb(Palette.pal5bit((byte)r), Palette.pal5bit((byte)g), Palette.pal5bit((byte)b)));
        }
        public static void m72_palette1_w(int offset, ushort data)
        {
            offset &= ~0x100;
            Generic.paletteram16[offset] = data;
            offset &= 0x0ff;
            changecolor(offset, Generic.paletteram16[offset + 0x000], Generic.paletteram16[offset + 0x200], Generic.paletteram16[offset + 0x400]);
        }
        public static void m72_palette2_w(int offset, ushort data)
        {
            offset &= ~0x100;
            Generic.paletteram16_2[offset] = data;
            offset &= 0x0ff;
            changecolor(offset + 256, Generic.paletteram16_2[offset + 0x000], Generic.paletteram16_2[offset + 0x200], Generic.paletteram16_2[offset + 0x400]);
        }
        public static void m72_videoram1_w(int offset, byte data)
        {
            m72_videoram1[offset] = data;
            fg_tilemap.tilemap_mark_tile_dirty(offset / 4);
        }
        public static void m72_videoram2_w(int offset, byte data)
        {
            m72_videoram2[offset] = data;
            bg_tilemap.tilemap_mark_tile_dirty(offset / 4);
        }
        public static void m72_videoram1_w(int offset, ushort data)
        {
            m72_videoram1[offset * 2] = (byte)data;
            m72_videoram1[offset * 2 + 1] = (byte)(data >> 8);
            fg_tilemap.tilemap_mark_tile_dirty(offset / 2);
        }
        public static void m72_videoram2_w(int offset, ushort data)
        {
            m72_videoram2[offset * 2] = (byte)data;
            m72_videoram2[offset * 2 + 1] = (byte)(data >> 8);
            bg_tilemap.tilemap_mark_tile_dirty(offset / 2);
        }
        public static void m72_irq_line_w(ushort data)
        {
            m72_raster_irq_position = data;
        }
        public static void m72_scrollx1_w(ushort data)
        {
            scrollx1 = data;
        }
        public static void m72_scrollx2_w(ushort data)
        {
            scrollx2 = data;
        }
        public static void m72_scrolly1_w(ushort data)
        {
            scrolly1 = data;
        }
        public static void m72_scrolly2_w(ushort data)
        {
            scrolly2 = data;
        }
        public static void m72_dmaon_w(ushort data)
        {
            //if (ACCESSING_BITS_0_7)
            Array.Copy(Generic.spriteram16, m72_spriteram, spriteram_size / 2);
        }
        public static void m72_port02_w(ushort data)
        {
            //if (ACCESSING_BITS_0_7)
            {
                //coin_counter_w(0,data & 0x01);
                //coin_counter_w(1,data & 0x02);
                //flip_screen_set(((data & 0x04) >> 2) ^ ((~input_port_read(machine, "DSW") >> 8) & 1));
                video_off = data & 0x08;
                if ((data & 0x10) != 0)
                {
                    Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_RESET, LineState.CLEAR_LINE);
                }
                else
                {
                    Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_RESET, LineState.ASSERT_LINE);
                }
            }
        }
        public static void rtype2_port02_w(ushort data)
        {
            //if (ACCESSING_BITS_0_7)
            {
                //coin_counter_w(0,data & 0x01);
                //coin_counter_w(1,data & 0x02);
                //flip_screen_set(((data & 0x04) >> 2) ^ ((~input_port_read(machine, "DSW") >> 8) & 1));
                video_off = data & 0x08;
            }
        }
        public static void majtitle_gfx_ctrl_w(ushort data)
        {
            //if (ACCESSING_BITS_8_15)
            {
                if ((data & 0xff00) != 0)
                {
                    majtitle_rowscroll = 1;
                }
                else
                {
                    majtitle_rowscroll = 0;
                }
            }
        }
        public static void m72_draw_sprites(RECT rect)
        {
            int offs;
            offs = 0;
            while (offs < spriteram_size / 2)
            {
                int code, color, sx, sy, flipx, flipy, w, h, x, y;
                code = m72_spriteram[offs + 1];
                color = m72_spriteram[offs + 2] & 0x0f;
                sx = -256 + (m72_spriteram[offs + 3] & 0x3ff);
                sy = 384 - (m72_spriteram[offs + 0] & 0x1ff);
                flipx = m72_spriteram[offs + 2] & 0x0800;
                flipy = m72_spriteram[offs + 2] & 0x0400;
                w = 1 << ((m72_spriteram[offs + 2] & 0xc000) >> 14);
                h = 1 << ((m72_spriteram[offs + 2] & 0x3000) >> 12);
                sy -= 16 * h;
                /*if (flip_screen_get())
                {
                    sx = 512 - 16*w - sx;
                    sy = 284 - 16*h - sy;
                    flipx = !flipx;
                    flipy = !flipy;
                }*/
                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        int c = code;
                        if (flipx != 0)
                        {
                            c += 8 * (w - 1 - x);
                        }
                        else
                        {
                            c += 8 * x;
                        }
                        if (flipy != 0)
                        {
                            c += h - 1 - y;
                        }
                        else
                        {
                            c += y;
                        }
                        Drawgfx.common_drawgfx_m72(M72.sprites1rom, c, color, flipx, flipy, sx + 16 * x, sy + 16 * y, rect);
                    }
                }
                offs += w * 4;
            }
        }
        public static void video_update_m72()
        {
            if (video_off!=0)
            {
                Array.Copy(uuB200, Video.bitmapbase[Video.curbitmap], 0x40000);
                return;
            }
            fg_tilemap.tilemap_set_scrollx(0, scrollx1);
            fg_tilemap.tilemap_set_scrolly(0, scrolly1);
            bg_tilemap.tilemap_set_scrollx(0, scrollx2);
            bg_tilemap.tilemap_set_scrolly(0, scrolly2);
            bg_tilemap.tilemap_draw_primask(Video.new_clip, 0x20, 0);
            fg_tilemap.tilemap_draw_primask(Video.new_clip, 0x20, 0);
            m72_draw_sprites(Video.new_clip);
            bg_tilemap.tilemap_draw_primask(Video.new_clip, 0x10, 0);
            fg_tilemap.tilemap_draw_primask(Video.new_clip, 0x10, 0);
        }
        public static void video_eof_m72()
        {

        }        
    }
}
