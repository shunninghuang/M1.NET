using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mame
{
    public partial class Tad
    {
        public static Tmap background_layer, foreground_layer, text_layer;
        public static int spriteram_size;
        public static ushort[] toki_background1_videoram16, toki_background2_videoram16, toki_scrollram16;
        public static void toki_control_w(int offset, ushort data)
        {
            Video.video_screen_update_partial(Video.video_screen_get_vpos() - 1);
            toki_scrollram16[offset] = data;
        }
        public static void toki_control_w1(int offset, byte data)
        {
            Video.video_screen_update_partial(Video.video_screen_get_vpos() - 1);
            toki_scrollram16[offset] = (ushort)((data << 8) | (toki_scrollram16[offset] & 0xff));
        }
        public static void toki_control_w2(int offset, byte data)
        {
            Video.video_screen_update_partial(Video.video_screen_get_vpos() - 1);
            toki_scrollram16[offset] = (ushort)((toki_scrollram16[offset] & 0xff00) | data);
        }
        public static void video_eof_toki()
        {
            Generic.buffer_spriteram16_w();
        }
        public static void video_start_toki()
        {
            int j;
            text_layer = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 8, 8, 32, 32);
            text_layer.total_elements = 0x1000;
            text_layer.pen_to_flags = new byte[1, 16];
            for (j = 0; j < 15; j++)
            {
                text_layer.pen_to_flags[0, j] = 0x10;
            }
            text_layer.pen_to_flags[0, 15] = 0;
            text_layer.tilemap_draw_instance3 = text_layer.tilemap_draw_instance_capcom_gng;
            text_layer.tile_update3 = text_layer.tile_update_tad_text;

            background_layer = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 16, 16, 32, 32);
            background_layer.total_elements = 0x2000;
            background_layer.pen_to_flags = new byte[1, 16];
            for (j = 0; j < 15; j++)
            {
                background_layer.pen_to_flags[0, j] = 0x10;
            }
            background_layer.pen_to_flags[0, 15] = 0;
            background_layer.tilemap_draw_instance3 = background_layer.tilemap_draw_instance_capcom_gng;
            background_layer.tile_update3 = background_layer.tile_update_tad_back;

            foreground_layer = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 16, 16, 32, 32);
            foreground_layer.total_elements = 0x1000;
            foreground_layer.pen_to_flags = new byte[1, 16];
            for (j = 0; j < 15; j++)
            {
                foreground_layer.pen_to_flags[0, j] = 0x10;
            }
            foreground_layer.pen_to_flags[0, 15] = 0;
            foreground_layer.tilemap_draw_instance3 = foreground_layer.tilemap_draw_instance_capcom_gng;
            foreground_layer.tile_update3 = foreground_layer.tile_update_tad_fore;
            
            Tilemap.lsTmap = new List<Tmap>();
            Tilemap.lsTmap.Add(text_layer);
            Tilemap.lsTmap.Add(background_layer);
            Tilemap.lsTmap.Add(foreground_layer);            
        }
        public static void toki_foreground_videoram16_w(int offset, ushort data)
        {
            Generic.videoram16[offset] = data;
            text_layer.tilemap_mark_tile_dirty(offset);
        }
        public static void toki_foreground_videoram16_w1(int offset, byte data)
        {
            Generic.videoram16[offset] = (ushort)((data << 8) | (Generic.videoram16[offset] & 0xff));
            text_layer.tilemap_mark_tile_dirty(offset);
        }
        public static void toki_foreground_videoram16_w2(int offset, byte data)
        {
            Generic.videoram16[offset] = (ushort)((Generic.videoram16[offset] & 0xff00) | data);
            text_layer.tilemap_mark_tile_dirty(offset);
        }
        public static void toki_background1_videoram16_w(int offset, ushort data)
        {
            toki_background1_videoram16[offset] = data;
            background_layer.tilemap_mark_tile_dirty(offset);
        }
        public static void toki_background1_videoram16_w1(int offset, byte data)
        {
            toki_background1_videoram16[offset] = (ushort)((data << 8) | (toki_background1_videoram16[offset] & 0xff));
            background_layer.tilemap_mark_tile_dirty(offset);
        }
        public static void toki_background1_videoram16_w2(int offset, byte data)
        {
            toki_background1_videoram16[offset] = (ushort)((toki_background1_videoram16[offset] & 0xff00) | data);
            background_layer.tilemap_mark_tile_dirty(offset);
        }
        public static void toki_background2_videoram16_w(int offset, ushort data)
        {
            toki_background2_videoram16[offset] = data;
            foreground_layer.tilemap_mark_tile_dirty(offset);
        }
        public static void toki_background2_videoram16_w1(int offset, byte data)
        {
            toki_background2_videoram16[offset] = (ushort)((data << 8) | (toki_background2_videoram16[offset] & 0xff));
            foreground_layer.tilemap_mark_tile_dirty(offset);
        }
        public static void toki_background2_videoram16_w2(int offset, byte data)
        {
            toki_background2_videoram16[offset] = (ushort)((toki_background2_videoram16[offset] & 0xff00) | data);
            foreground_layer.tilemap_mark_tile_dirty(offset);
        }
        public static void toki_draw_sprites(RECT cliprect)
        {
            int x, y, xoffs, yoffs, tile, flipx, flipy, color, offs;
            spriteram_size = 0x800;
            for (offs = (spriteram_size / 2) - 4; offs >= 0; offs -= 4)
            {
                if ((Generic.buffered_spriteram16[offs + 2] != 0xf000) && (Generic.buffered_spriteram16[offs] != 0xffff))
                {
                    xoffs = (Generic.buffered_spriteram16[offs] & 0xf0);
                    x = (Generic.buffered_spriteram16[offs + 2] + xoffs) & 0x1ff;
                    if (x > 256)
                    {
                        x -= 512;
                    }
                    yoffs = (Generic.buffered_spriteram16[offs] & 0xf) << 4;
                    y = (Generic.buffered_spriteram16[offs + 3] + yoffs) & 0x1ff;
                    if (y > 256)
                    {
                        y -= 512;
                    }
                    color = Generic.buffered_spriteram16[offs + 1] >> 12;
                    flipx = Generic.buffered_spriteram16[offs] & 0x100;
                    flipy = 0;
                    tile = (Generic.buffered_spriteram16[offs + 1] & 0xfff) + ((Generic.buffered_spriteram16[offs + 2] & 0x8000) >> 3);
                    if (Generic.flip_screen_get() != 0)
                    {
                        x = 240 - x;
                        y = 240 - y;
                        if (flipx != 0)
                        {
                            flipx = 0;
                        }
                        else
                        {
                            flipx = 1;
                        }
                        flipy = 1;
                    }
                    Drawgfx.common_drawgfx_toki(gfx2rom, tile, color, flipx, flipy, x, y, cliprect);
                }
            }
        }
        public static void tokib_draw_sprites(RECT cliprect)
        {
            int x, y, tile, flipx, color, offs;
            spriteram_size = 0x800;
            for (offs = 0; offs < spriteram_size / 2; offs += 4)
            {
                if (Generic.buffered_spriteram16[offs] == 0xf100)
                {
                    break;
                }
                if (Generic.buffered_spriteram16[offs + 2] != 0)
                {
                    x = Generic.buffered_spriteram16[offs + 3] & 0x1ff;
                    if (x > 256)
                    {
                        x -= 512;
                    }
                    y = Generic.buffered_spriteram16[offs] & 0x1ff;
                    if (y > 256)
                    {
                        y = (512 - y) + 240;
                    }
                    else
                    {
                        y = 240 - y;
                    }
                    flipx = Generic.buffered_spriteram16[offs + 1] & 0x4000;
                    tile = Generic.buffered_spriteram16[offs + 1] & 0x1fff;
                    color = Generic.buffered_spriteram16[offs + 2] >> 12;
                    Drawgfx.common_drawgfx_toki(gfx2rom, tile, color, flipx, 0, x, y - 1, cliprect);
                }
            }
        }
        public static void video_update_toki()
        {
            int background_y_scroll, foreground_y_scroll, background_x_scroll, foreground_x_scroll;
            background_x_scroll = ((toki_scrollram16[0x06] & 0x7f) << 1)
                                         | ((toki_scrollram16[0x06] & 0x80) >> 7)
                                         | ((toki_scrollram16[0x05] & 0x10) << 4);
            background_y_scroll = ((toki_scrollram16[0x0d] & 0x10) << 4) + ((toki_scrollram16[0x0e] & 0x7f) << 1) + ((toki_scrollram16[0x0e] & 0x80) >> 7);
            background_layer.tilemap_set_scrollx(0, background_x_scroll);
            background_layer.tilemap_set_scrolly(0, background_y_scroll);
            foreground_x_scroll = ((toki_scrollram16[0x16] & 0x7f) << 1)
                                         | ((toki_scrollram16[0x16] & 0x80) >> 7)
                                         | ((toki_scrollram16[0x15] & 0x10) << 4);
            foreground_y_scroll = ((toki_scrollram16[0x1d] & 0x10) << 4) + ((toki_scrollram16[0x1e] & 0x7f) << 1) + ((toki_scrollram16[0x1e] & 0x80) >> 7);
            foreground_layer.tilemap_set_scrollx(0, foreground_x_scroll);
            foreground_layer.tilemap_set_scrolly(0, foreground_y_scroll);
            Generic.flip_screen_set((toki_scrollram16[0x28] & 0x8000) == 0 ? 1 : 0);
            if ((toki_scrollram16[0x28] & 0x100) != 0)
            {
                background_layer.tilemap_draw_primask(Video.new_clip, 0, 0);
                foreground_layer.tilemap_draw_primask(Video.new_clip, 0x10, 0);
            }
            else
            {
                foreground_layer.tilemap_draw_primask(Video.new_clip, 0, 0);
                background_layer.tilemap_draw_primask(Video.new_clip, 0x10, 0);
            }
            toki_draw_sprites(Video.new_clip);
            text_layer.tilemap_draw_primask(Video.new_clip, 0x10, 0);
        }
        public static void video_update_tokib()
        {
            foreground_layer.tilemap_set_scroll_rows(1);
            background_layer.tilemap_set_scroll_rows(1);
            background_layer.tilemap_set_scrolly(0, toki_scrollram16[0] + 1);
            background_layer.tilemap_set_scrollx(0, toki_scrollram16[1] - 0x103);
            foreground_layer.tilemap_set_scrolly(0, toki_scrollram16[2] + 1);
            foreground_layer.tilemap_set_scrollx(0, toki_scrollram16[3] - 0x101);
            if ((toki_scrollram16[3] & 0x2000) != 0)
            {
                background_layer.tilemap_draw_primask(Video.new_clip, 0, 0);
                foreground_layer.tilemap_draw_primask(Video.new_clip, 0x10, 0);
            }
            else
            {
                foreground_layer.tilemap_draw_primask(Video.new_clip, 0, 0);
                background_layer.tilemap_draw_primask(Video.new_clip, 0x10, 0);
            }
            tokib_draw_sprites(Video.new_clip);
            text_layer.tilemap_draw_primask(Video.new_clip, 0x10, 0);
        }
    }
}
