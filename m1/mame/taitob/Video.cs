using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mame
{
    public partial class Taitob
    {
        public static ushort[][] framebuffer;
        public static ushort[] taitob_scroll, TC0180VCU_ram, taitob_spriteram, taitob_pixelram, pixel_bitmap;
        public static ushort[] bg_rambank, fg_rambank, pixel_scroll, TC0180VCU_ctrl;
        public static ushort tx_rambank, m_realpunc_video_ctrl;
        public static byte framebuffer_page, video_control;
        public static int b_bg_color_base = 0, b_fg_color_base = 0, b_sp_color_base = 0, b_tx_color_base = 0;
        public static byte[] TC0220IOC_regs, TC0640FIO_regs;
        public static byte TC0220IOC_port;
        public static RECT cliprect;
        public static ushort[] uuB0000;
        public static void taitob_video_control(byte data)
        {
            video_control = data;
            if ((video_control & 0x80) != 0)
            {
                framebuffer_page = (byte)((~video_control & 0x40) >> 6);
            }
            Tmap.tilemap_set_flip(null, (byte)((video_control & 0x10) != 0 ? (Tilemap.TILEMAP_FLIPX | Tilemap.TILEMAP_FLIPY) : 0));
        }
        public static ushort TC0180VCU_word_r(int offset)
        {
            return TC0180VCU_ram[offset];
        }
        public static void TC0180VCU_word_w1(int offset, byte data)
        {
            int row, col;
            TC0180VCU_ram[offset] = (ushort)((data << 8) | (byte)TC0180VCU_ram[offset]);
            if ((offset & 0x7000) == fg_rambank[0] || (offset & 0x7000) == fg_rambank[1])
            {
                fg_tilemap.tilemap_mark_tile_dirty(offset & 0x0fff);
            }
            if ((offset & 0x7000) == bg_rambank[0] || (offset & 0x7000) == bg_rambank[1])
            {
                bg_tilemap.tilemap_mark_tile_dirty(offset & 0x0fff);
            }
            if ((offset & 0x7800) == tx_rambank)
            {
                tx_tilemap.tilemap_mark_tile_dirty(offset & 0x07ff);
            }
        }
        public static void TC0180VCU_word_w2(int offset, byte data)
        {
            int row, col;
            TC0180VCU_ram[offset] = (ushort)((TC0180VCU_ram[offset] & 0xff00) | (byte)data);
            if ((offset & 0x7000) == fg_rambank[0] || (offset & 0x7000) == fg_rambank[1])
            {
                fg_tilemap.tilemap_mark_tile_dirty(offset & 0x0fff);
            }
            if ((offset & 0x7000) == bg_rambank[0] || (offset & 0x7000) == bg_rambank[1])
            {
                bg_tilemap.tilemap_mark_tile_dirty(offset & 0x0fff);
            }
            if ((offset & 0x7800) == tx_rambank)
            {
                tx_tilemap.tilemap_mark_tile_dirty(offset & 0x07ff);
            }
        }
        public static void TC0180VCU_word_w(int offset, ushort data)
        {
            int row, col;
            TC0180VCU_ram[offset] = data;
            if ((offset & 0x7000) == fg_rambank[0] || (offset & 0x7000) == fg_rambank[1])
            {
                fg_tilemap.tilemap_mark_tile_dirty(offset & 0x0fff);
            }
            if ((offset & 0x7000) == bg_rambank[0] || (offset & 0x7000) == bg_rambank[1])
            {
                bg_tilemap.tilemap_mark_tile_dirty(offset & 0x0fff);
            }
            if ((offset & 0x7800) == tx_rambank)
            {
                tx_tilemap.tilemap_mark_tile_dirty(offset & 0x07ff);
            }
        }
        public static void realpunc_video_ctrl_w1(byte data)
        {
            m_realpunc_video_ctrl = (ushort)((data << 8) | (m_realpunc_video_ctrl & 0xff));
        }
        public static void realpunc_video_ctrl_w2(byte data)
        {
            m_realpunc_video_ctrl = (ushort)((m_realpunc_video_ctrl & 0xff00) | data);
        }
        public static void realpunc_video_ctrl_w(ushort data)
        {
            m_realpunc_video_ctrl = data;
        }
        public static void video_start_taitob_core()
        {
            int i;
            Taito.taitoic_init();
            uuB0000 = new ushort[0x200 * 0x100];
            for (i = 0; i < 0x20000; i++)
            {
                uuB0000[i] = 0x0;
            }
            cliprect = new RECT();
            cliprect.min_x = 0;
            cliprect.max_x = 319;
            cliprect.min_y = 16;
            cliprect.max_y = 239;
            framebuffer = new ushort[2][];
            for (i = 0; i < 2; i++)
            {
                framebuffer[i] = new ushort[0x200 * 0x100];
            }
            bg_tilemap = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 16, 16, 64, 64);
            fg_tilemap = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 16, 16, 64, 64);
            tx_tilemap = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 8, 8, 64, 32);

            bg_tilemap.total_elements = gfx1rom.Length / 0x100;
            bg_tilemap.pen_to_flags = new byte[1, 0x10];
            for (i = 0; i < 16; i++)
            {
                bg_tilemap.pen_to_flags[0, i] = 0x10;
            }
            bg_tilemap.tilemap_draw_instance3 = bg_tilemap.tilemap_draw_instance_cps;
            bg_tilemap.tile_update3 = bg_tilemap.tile_update_taitob_bg;

            fg_tilemap.total_elements = gfx1rom.Length / 0x100;
            fg_tilemap.pen_to_flags = new byte[1, 16];
            fg_tilemap.pen_to_flags[0, 0] = 0;
            for (i = 1; i < 16; i++)
            {
                fg_tilemap.pen_to_flags[0, i] = 0x10;
            }
            fg_tilemap.tilemap_draw_instance3 = fg_tilemap.tilemap_draw_instance_cps;
            fg_tilemap.tile_update3 = fg_tilemap.tile_update_taitob_fg;

            tx_tilemap.total_elements = gfx1rom.Length / 0x40;
            tx_tilemap.pen_to_flags = new byte[1, 16];
            tx_tilemap.pen_to_flags[0, 0] = 0;
            for (i = 1; i < 16; i++)
            {
                tx_tilemap.pen_to_flags[0, i] = 0x10;
            }
            tx_tilemap.tilemap_draw_instance3 = tx_tilemap.tilemap_draw_instance_cps;
            tx_tilemap.tile_update3 = tx_tilemap.tile_update_taitob_tx;

            bg_tilemap.tilemap_set_scrolldx(0, 24 * 8);
            fg_tilemap.tilemap_set_scrolldx(0, 24 * 8);
            tx_tilemap.tilemap_set_scrolldx(0, 24 * 8);
            Tilemap.lsTmap = new List<Tmap>();
            Tilemap.lsTmap.Add(bg_tilemap);
            Tilemap.lsTmap.Add(fg_tilemap);
            Tilemap.lsTmap.Add(tx_tilemap);



            pixel_bitmap = null;  /* only hitice needs this */

            
        }
        public static void video_start_taitob_color_order0()
        {
            b_bg_color_base = 0xc0;
            b_fg_color_base = 0x80;
            b_sp_color_base = 0x40 * 16;
            b_tx_color_base = 0x00;
            video_start_taitob_core();
        }
        public static void video_start_taitob_color_order1()
        {
            b_bg_color_base = 0x00;
            b_fg_color_base = 0x40;
            b_sp_color_base = 0x80 * 16;
            b_tx_color_base = 0xc0;
            video_start_taitob_core();
        }
        public static void video_start_taitob_color_order2()
        {
            b_bg_color_base = 0x30;
            b_fg_color_base = 0x20;
            b_sp_color_base = 0x10 * 16;
            b_tx_color_base = 0x00;
            video_start_taitob_core();
        }
        public static void video_start_hitice()
        {
            video_start_taitob_color_order0();
            taitob_pixelram = new ushort[0x40000];
            pixel_bitmap = new ushort[0x400 * 0x200];
        }
        public static void video_reset_hitice()
        {
            hitice_clear_pixel_bitmap();
        }
        public static ushort TC0180VCU_framebuffer_word_r(int offset)
        {
            int sy = offset >> 8;
            int sx = 2 * (offset & 0xff);
            return (ushort)((framebuffer[sy >> 8][(sy & 0xff) * 0x200 + sx] << 8) | framebuffer[sy >> 8][(sy & 0xff) * 0x200 + sx + 1]);
        }
        public static void TC0180VCU_framebuffer_word_w1(int offset, byte data)
        {
            int sy = offset >> 8;
            int sx = 2 * (offset & 0xff);
            if ((sy & 0xff) * 0x200 + sx == 0xf038 && data == 0)
            {
                int i1 = 1;
            }
            framebuffer[sy >> 8][(sy & 0xff) * 0x200 + sx] = (ushort)data;
        }
        public static void TC0180VCU_framebuffer_word_w2(int offset, byte data)
        {
            int sy = offset >> 8;
            int sx = 2 * (offset & 0xff);
            framebuffer[sy >> 8][(sy & 0xff) * 0x200 + sx + 1] = (ushort)data;
        }
        public static void TC0180VCU_framebuffer_word_w(int offset, ushort data)
        {
            int sy = offset >> 8;
            int sx = 2 * (offset & 0xff);
            if ((sy & 0xff) * 0x200 + sx == 0xf038 && (data >> 8) == 0)
            {
                int i1 = 1;
            }
            framebuffer[sy >> 8][(sy & 0xff) * 0x200 + sx] = (ushort)(data >> 8);
            framebuffer[sy >> 8][(sy & 0xff) * 0x200 + sx + 1] = (ushort)(data & 0xff);
        }
        public static ushort taitob_v_control_r(int offset)
        {
            return TC0180VCU_ctrl[offset];
        }
        public static void taitob_v_control_w1(int offset, byte data)
        {
            ushort oldword = TC0180VCU_ctrl[offset];
            TC0180VCU_ctrl[offset] = (ushort)((data << 8) | (TC0180VCU_ctrl[offset] & 0xff));
            switch (offset)
            {
                case 0:
                    if (oldword != TC0180VCU_ctrl[offset])
                    {
                        fg_tilemap.all_tiles_dirty = true;
                        fg_rambank[0] = (ushort)(((TC0180VCU_ctrl[offset] >> 8) & 0x0f) << 12);
                        fg_rambank[1] = (ushort)(((TC0180VCU_ctrl[offset] >> 12) & 0x0f) << 12);
                    }
                    break;
                case 1:
                    if (oldword != TC0180VCU_ctrl[offset])
                    {
                        bg_tilemap.all_tiles_dirty = true;
                        bg_rambank[0] = (ushort)(((TC0180VCU_ctrl[offset] >> 8) & 0x0f) << 12);
                        bg_rambank[1] = (ushort)(((TC0180VCU_ctrl[offset] >> 12) & 0x0f) << 12);
                    }
                    break;
                case 4:
                case 5:
                    if (oldword != TC0180VCU_ctrl[offset])
                    {
                        tx_tilemap.all_tiles_dirty = true;
                    }
                    break;
                case 6:
                    if (oldword != TC0180VCU_ctrl[offset])
                    {
                        tx_tilemap.all_tiles_dirty = true;
                        tx_rambank = (ushort)(((TC0180VCU_ctrl[offset] >> 8) & 0x0f) << 11);
                    }
                    break;
                case 7:
                    taitob_video_control((byte)((TC0180VCU_ctrl[offset] >> 8) & 0xff));
                    break;
                default:
                    break;
            }
        }
        public static void taitob_v_control_w2(int offset, byte data)
        {
            TC0180VCU_ctrl[offset] = (ushort)((TC0180VCU_ctrl[offset] & 0xff00) | data);
        }
        public static void taitob_v_control_w(int offset, ushort data)
        {
            ushort oldword = TC0180VCU_ctrl[offset];
            TC0180VCU_ctrl[offset] = data;
            switch (offset)
            {
                case 0:
                    if (oldword != TC0180VCU_ctrl[offset])
                    {
                        fg_tilemap.all_tiles_dirty = true;
                        fg_rambank[0] = (ushort)(((TC0180VCU_ctrl[offset] >> 8) & 0x0f) << 12);
                        fg_rambank[1] = (ushort)(((TC0180VCU_ctrl[offset] >> 12) & 0x0f) << 12);
                    }
                    break;
                case 1:
                    if (oldword != TC0180VCU_ctrl[offset])
                    {
                        bg_tilemap.all_tiles_dirty = true;
                        bg_rambank[0] = (ushort)(((TC0180VCU_ctrl[offset] >> 8) & 0x0f) << 12);
                        bg_rambank[1] = (ushort)(((TC0180VCU_ctrl[offset] >> 12) & 0x0f) << 12);
                    }
                    break;
                case 4:
                case 5:
                    if (oldword != TC0180VCU_ctrl[offset])
                    {
                        tx_tilemap.all_tiles_dirty = true;
                    }
                    break;
                case 6:
                    if (oldword != TC0180VCU_ctrl[offset])
                    {
                        tx_tilemap.all_tiles_dirty = true;
                        tx_rambank = (ushort)(((TC0180VCU_ctrl[offset] >> 8) & 0x0f) << 11);
                    }
                    break;
                case 7:
                    taitob_video_control((byte)((TC0180VCU_ctrl[offset] >> 8) & 0xff));
                    break;
                default:
                    break;
            }
        }
        public static void hitice_pixelram_w1(int offset, byte data)
        {
            int sy = offset >> 9;
            int sx = offset & 0x1ff;
            taitob_pixelram[offset] = (ushort)((data << 8) | (taitob_pixelram[offset] & 0xff));
        }
        public static void hitice_pixelram_w2(int offset, byte data)
        {
            int sy = offset >> 9;
            int sx = offset & 0x1ff;
            taitob_pixelram[offset] = (ushort)((taitob_pixelram[offset] & 0xff00) | data);
            pixel_bitmap[sy * 0x400 + 2 * sx + 0] = (ushort)(b_fg_color_base * 16 + data);
            pixel_bitmap[sy * 0x400 + 2 * sx + 1] = (ushort)(b_fg_color_base * 16 + data);
        }
        public static void hitice_pixelram_w(int offset, ushort data)
        {
            int sy = offset >> 9;
            int sx = offset & 0x1ff;
            taitob_pixelram[offset] = data;
            pixel_bitmap[sy * 0x400 + 2 * sx + 0] = (ushort)(b_fg_color_base * 16 + (data & 0xff));
            pixel_bitmap[sy * 0x400 + 2 * sx + 1] = (ushort)(b_fg_color_base * 16 + (data & 0xff));
        }
        public static void hitice_pixel_scroll_w1(int offset, byte data)
        {
            pixel_scroll[offset] = (ushort)((data << 8) | (pixel_scroll[offset] & 0xff));
        }
        public static void hitice_pixel_scroll_w2(int offset, byte data)
        {
            pixel_scroll[offset] = (ushort)((pixel_scroll[offset] & 0xff00) | data);
        }
        public static void hitice_pixel_scroll_w(int offset, ushort data)
        {
            pixel_scroll[offset] = data;
        }
        public static void hitice_clear_pixel_bitmap()
        {
            int i;

            for (i = 0; i < 0x40000; i++)
            {
                hitice_pixelram_w(i, 0);
            }
        }
        public static RECT sect_rect(RECT dst, RECT src)
        {
            RECT dst2 = dst;
            if (src.min_x > dst.min_x) dst2.min_x = src.min_x;
            if (src.max_x < dst.max_x) dst2.max_x = src.max_x;
            if (src.min_y > dst.min_y) dst2.min_y = src.min_y;
            if (src.max_y < dst.max_y) dst2.max_y = src.max_y;
            return dst2;
        }
        public static void draw_sprites(RECT cliprect)
        {
            int x, y, xlatch = 0, ylatch = 0, x_no = 0, y_no = 0, x_num = 0, y_num = 0, big_sprite = 0;
            int offs, code, color, flipx, flipy;
            uint data, zoomx, zoomy, zx, zy, zoomxlatch = 0, zoomylatch = 0;
            for (offs = (0x1980 - 16) / 2; offs >= 0; offs -= 8)
            {
                code = taitob_spriteram[offs];
                color = taitob_spriteram[offs + 1];
                flipx = color & 0x4000;
                flipy = color & 0x8000;
                color = (color & 0x3f) * 16;
                x = taitob_spriteram[offs + 2] & 0x3ff;
                y = taitob_spriteram[offs + 3] & 0x3ff;
                if (x >= 0x200) x -= 0x400;
                if (y >= 0x200) y -= 0x400;
                data = taitob_spriteram[offs + 5];
                if (data != 0)
                {
                    if (big_sprite == 0)
                    {
                        x_num = (int)((data >> 8) & 0xff);
                        y_num = (int)((data) & 0xff);
                        x_no = 0;
                        y_no = 0;
                        xlatch = x;
                        ylatch = y;
                        data = taitob_spriteram[offs + 4];
                        zoomxlatch = (data >> 8) & 0xff;
                        zoomylatch = (data) & 0xff;
                        big_sprite = 1;
                    }
                }
                data = taitob_spriteram[offs + 4];
                zoomx = (data >> 8) & 0xff;
                zoomy = (data) & 0xff;
                zx = (0x100 - zoomx) / 16;
                zy = (0x100 - zoomy) / 16;
                if (big_sprite != 0)
                {
                    zoomx = zoomxlatch;
                    zoomy = zoomylatch;
                    x = (int)(xlatch + x_no * (0x100 - zoomx) / 16);
                    y = (int)(ylatch + y_no * (0x100 - zoomy) / 16);
                    zx = (uint)(xlatch + (x_no + 1) * (0x100 - zoomx) / 16 - x);
                    zy = (uint)(ylatch + (y_no + 1) * (0x100 - zoomy) / 16 - y);
                    y_no++;
                    if (y_no > y_num)
                    {
                        y_no = 0;
                        x_no++;
                        if (x_no > x_num)
                            big_sprite = 0;
                    }
                }
                if ((zoomx != 0) || (zoomy != 0))
                {
                    Drawgfx.common_drawgfxzoom_taitob(gfx1rom, code, color, flipx, flipy, x, y, cliprect, 0, (int)((zx << 16) / 16), (int)((zy << 16) / 16));
                }
                else
                {
                    Drawgfx.common_drawgfx_taitob(gfx1rom, code, color, flipx, flipy, x, y, cliprect);
                }
            }
        }
        public static void TC0180VCU_tilemap_draw(RECT cliprect, Tmap tmap, int plane)
        {
            RECT my_clip;
            int i;
            int scrollx, scrolly;
            int lines_per_block;	/* number of lines scrolled by the same amount (per one scroll value) */
            int number_of_blocks;	/* number of such blocks per _screen_ (256 lines) */
            lines_per_block = 256 - (TC0180VCU_ctrl[2 + plane] >> 8);
            number_of_blocks = 256 / lines_per_block;
            my_clip.min_x = cliprect.min_x;
            my_clip.max_x = cliprect.max_x;
            for (i = 0; i < number_of_blocks; i++)
            {
                scrollx = taitob_scroll[plane * 0x200 + i * 2 * lines_per_block];
                scrolly = taitob_scroll[plane * 0x200 + i * 2 * lines_per_block + 1];
                my_clip.min_y = i * lines_per_block;
                my_clip.max_y = (i + 1) * lines_per_block - 1;
                if ((video_control & 0x10) != 0)   /*flip screen*/
                {
                    my_clip.min_y = 0x100 - 1 - (i + 1) * lines_per_block - 1;
                    my_clip.max_y = 0x100 - 1 - i * lines_per_block;
                }
                my_clip = sect_rect(my_clip, cliprect);
                if (my_clip.min_y <= my_clip.max_y)
                {
                    tmap.tilemap_set_scrollx(0, -scrollx);
                    tmap.tilemap_set_scrolly(0, -scrolly);
                    tmap.tilemap_draw_primask(my_clip, 0x10, 0);
                }
            }
        }
        public static void draw_framebuffer(RECT cliprect, int priority)
        {
            RECT myclip = cliprect;
            int x, y;
            priority <<= 4;
            if ((video_control & 0x08) != 0)
            {
                if (priority != 0)
                {
                    return;
                }
                if ((video_control & 0x10) != 0)   /*flip screen*/
                {
                    for (y = myclip.min_y; y <= myclip.max_y; y++)
                    {
                        for (x = myclip.min_x; x <= myclip.max_x; x++)
                        {
                            ushort c = framebuffer[framebuffer_page][y * 512 + x];
                            if (c != 0)
                            {
                                Video.bitmapbase[Video.curbitmap][(255 - y) * 512 + 319 - x] = (ushort)(b_sp_color_base + c);
                            }
                        }
                    }
                }
                else
                {
                    for (y = myclip.min_y; y <= myclip.max_y; y++)
                    {
                        for (x = myclip.min_x; x <= myclip.max_x; x++)
                        {
                            ushort c = framebuffer[framebuffer_page][y * 512 + x];
                            if (c != 0)
                            {
                                Video.bitmapbase[Video.curbitmap][y * 512 + x] = (ushort)(b_sp_color_base + c);
                            }
                        }
                    }
                }
            }
            else
            {
                if ((video_control & 0x10) != 0)   /*flip screen*/
                {
                    for (y = myclip.min_y; y <= myclip.max_y; y++)
                    {
                        for (x = myclip.min_x; x <= myclip.max_x; x++)
                        {
                            ushort c = framebuffer[framebuffer_page][y * 512 + x];
                            if ((c != 0) && ((c & 0x10) == priority))
                            {
                                Video.bitmapbase[Video.curbitmap][(255 - y) * 512 + 319 - x] = (ushort)(b_sp_color_base + c);
                            }
                        }
                    }
                }
                else
                {
                    for (y = myclip.min_y; y <= myclip.max_y; y++)
                    {
                        for (x = myclip.min_x; x <= myclip.max_x; x++)
                        {
                            ushort c = framebuffer[framebuffer_page][y * 512 + x];
                            if ((c != 0) && ((c & 0x10) == priority))
                            {
                                Video.bitmapbase[Video.curbitmap][y * 512 + x] = (ushort)(b_sp_color_base + c);
                            }
                        }
                    }
                }
            }
        }
        public static void video_update_taitob()
        {
            if ((video_control & 0x20) == 0)
            {
                Array.Copy(uuB0000, Video.bitmapbase[Video.curbitmap], 0x20000);
                return;
            }
            TC0180VCU_tilemap_draw(cliprect, bg_tilemap, 1);
            draw_framebuffer(cliprect, 1);
            TC0180VCU_tilemap_draw(cliprect, fg_tilemap, 0);
            if (pixel_bitmap != null)
            {
                int scrollx = -2 * pixel_scroll[0];
                int scrolly = -pixel_scroll[1];
                //copyscrollbitmap_trans(bitmap, pixel_bitmap, 1, &scrollx, 1, &scrolly, cliprect, b_fg_color_base * 16);
            }
            draw_framebuffer(cliprect, 0);
            tx_tilemap.tilemap_draw_primask(cliprect, 0x10, 0);
        }
        public static void video_eof_taitob()
        {
            if ((~video_control & 0x01) != 0)
            {
                Array.Copy(uuB0000, framebuffer[framebuffer_page], 0x20000);
            }
            if ((~video_control & 0x80) != 0)
            {
                framebuffer_page ^= 1;
            }
            draw_sprites(cliprect);
        }
    }
}
