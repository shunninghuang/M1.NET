using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mame
{
    public partial class Tehkan
    {
        public static byte[] pbaction_videoram2, pbaction_colorram2;
        public static int scroll;        
        public static RECT cliprect;
        public static void pbaction_videoram_w(int offset, byte data)
        {
            Generic.videoram[offset] = data;
            bg_tilemap.tilemap_mark_tile_dirty(offset);
        }
        public static void pbaction_colorram_w(int offset, byte data)
        {
            Generic.colorram[offset] = data;
            bg_tilemap.tilemap_mark_tile_dirty(offset);
        }
        public static void pbaction_videoram2_w(int offset, byte data)
        {
            pbaction_videoram2[offset] = data;
            fg_tilemap.tilemap_mark_tile_dirty(offset);
        }
        public static void pbaction_colorram2_w(int offset, byte data)
        {
            pbaction_colorram2[offset] = data;
            fg_tilemap.tilemap_mark_tile_dirty(offset);
        }
        public static void pbaction_scroll_w(byte data)
        {
            scroll = data - 3;
            if (Generic.flip_screen_get() != 0)
            {
                scroll = -scroll;
            }
            bg_tilemap.tilemap_set_scrollx(0, scroll);
            fg_tilemap.tilemap_set_scrollx(0, scroll);
        }
        public static void pbaction_flipscreen_w(byte data)
        {
            Generic.flip_screen_set(data & 0x01);
        }
        public static void video_start_pbaction()
        {
            int i;
            bg_tilemap = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 8, 8, 32, 32);
            bg_tilemap.total_elements = gfx2rom.Length / 0x40;
            bg_tilemap.pen_to_flags = new byte[1, 16];
            for (i = 0; i < 16; i++)
            {
                bg_tilemap.pen_to_flags[0, i] = 0x10;
            }
            bg_tilemap.tilemap_draw_instance3 = bg_tilemap.tilemap_draw_instance_capcom_gng;
            bg_tilemap.tile_update3 = bg_tilemap.tile_update_pbaction_bg;

            fg_tilemap = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 8, 8, 32, 32);
            fg_tilemap.total_elements = gfx1rom.Length / 0x40;
            fg_tilemap.pen_to_flags = new byte[1, 16];
            fg_tilemap.pen_to_flags[0, 0] = 0;
            for (i = 1; i < 16; i++)
            {
                fg_tilemap.pen_to_flags[0, i] = 0x10;
            }
            fg_tilemap.tilemap_draw_instance3 = fg_tilemap.tilemap_draw_instance_capcom_gng;
            fg_tilemap.tile_update3 = fg_tilemap.tile_update_pbaction_fg;
            Tilemap.lsTmap = new List<Tmap>();
            Tilemap.lsTmap.Add(bg_tilemap);
            Tilemap.lsTmap.Add(fg_tilemap);

            cliprect = new RECT();
            cliprect.min_x = 0;
            cliprect.max_x = 0xff;
            cliprect.min_y = 0x10;
            cliprect.max_y = 0xef;
        }
        public static void draw_sprites(RECT cliprect)
        {
            int offs;
            for (offs = 0x80 - 4; offs >= 0; offs -= 4)
            {
                int sx, sy, flipx, flipy;
                if (offs > 0 && (Generic.spriteram[offs - 4] & 0x80) != 0)
                {
                    continue;
                }
                sx = Generic.spriteram[offs + 3];
                if ((Generic.spriteram[offs] & 0x80) != 0)
                {
                    sy = 225 - Generic.spriteram[offs + 2];
                }
                else
                {
                    sy = 241 - Generic.spriteram[offs + 2];
                }
                flipx = Generic.spriteram[offs + 1] & 0x40;
                flipy = Generic.spriteram[offs + 1] & 0x80;
                if (Generic.flip_screen_get() != 0)
                {
                    if ((Generic.spriteram[offs] & 0x80) != 0)
                    {
                        sx = 224 - sx;
                        sy = 225 - sy;
                    }
                    else
                    {
                        sx = 240 - sx;
                        sy = 241 - sy;
                    }
                    flipx = (flipx == 0 ? 1 : 0);
                    flipy = (flipy == 0 ? 1 : 0);
                }
                if ((Generic.spriteram[offs] & 0x80) != 0)
                {
                    Drawgfx.common_drawgfx_pbaction(gfx32rom, 32, 32, 32, 0x20, Generic.spriteram[offs], Generic.spriteram[offs + 1] & 0x0f, flipx, flipy, sx + (Generic.flip_screen_get() != 0 ? scroll : -scroll), sy, cliprect);
                }
                else
                {
                    Drawgfx.common_drawgfx_pbaction(gfx3rom, 16, 16, 16, 0x80, Generic.spriteram[offs], Generic.spriteram[offs + 1] & 0x0f, flipx, flipy, sx + (Generic.flip_screen_get() != 0 ? scroll : -scroll), sy, cliprect);
                }
            }
        }
        public static void video_update_pbaction()
        {
            bg_tilemap.tilemap_draw_primask(cliprect, 0x10, 0);
            draw_sprites(cliprect);
            fg_tilemap.tilemap_draw_primask(cliprect, 0x10, 0);
        }
        public static void video_eof_pbaction()
        {

        }
    }
}
