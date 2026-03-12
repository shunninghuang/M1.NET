using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Technos
    {
        public static int background_scan(int col, int row, int num_cols, int num_rows)
        {
            return (col & 0x0f) + ((row & 0x0f) << 4) + ((col & 0x10) << 4) + ((row & 0x10) << 5);
        }
        public static void video_start_ddragon()
        {
            int i;
            bg_tilemap = Tmap.tilemap_create(background_scan, 16, 16, 32, 32);
            fg_tilemap = Tmap.tilemap_create(Tmap.tilemap_scan_rows, 8, 8, 32, 32);

            bg_tilemap.total_elements = gfx2rom.Length / 0x40;
            bg_tilemap.pen_to_flags = new byte[1, 16];
            for (i = 0; i < 16; i++)
            {
                bg_tilemap.pen_to_flags[0, i] = 0x10;
            }
            bg_tilemap.tilemap_draw_instance3 = bg_tilemap.tilemap_draw_instance_capcom_ddragon;
            bg_tilemap.tile_update3 = bg_tilemap.tile_update_ddragon_bg;

            fg_tilemap.total_elements = gfx1rom.Length / 0x40;
            fg_tilemap.pen_to_flags = new byte[1, 16];
            fg_tilemap.pen_to_flags[0, 0] = 0;
            for (i = 1; i < 16; i++)
            {
                fg_tilemap.pen_to_flags[0, i] = 0x10;
            }
            fg_tilemap.tilemap_draw_instance3 = fg_tilemap.tilemap_draw_instance_capcom_ddragon;
            fg_tilemap.tile_update3 = fg_tilemap.tile_update_ddragon_fg;
            Tilemap.lsTmap = new List<Tmap>();
            Tilemap.lsTmap.Add(bg_tilemap);
            Tilemap.lsTmap.Add(fg_tilemap);

            gfxtotalelement = gfx2rom.Length / 0x100;
            fg_tilemap.tilemap_set_scrolldx(0, 384 - 256);
            bg_tilemap.tilemap_set_scrolldx(0, 384 - 256);
            fg_tilemap.tilemap_set_scrolldy(-8, -8);
            bg_tilemap.tilemap_set_scrolldy(-8, -8);
        }
        public static void ddragon_bgvideoram_w(int offset, byte data)
        {
            ddragon_bgvideoram[offset] = data;
            bg_tilemap.tilemap_mark_tile_dirty(offset / 2);
        }
        public static void ddragon_fgvideoram_w(int offset, byte data)
        {
            ddragon_fgvideoram[offset] = data;
            fg_tilemap.tilemap_mark_tile_dirty(offset / 2);
        }
        public static void draw_sprites(RECT cliprect)
        {
            byte[] srcbyte = new byte[0x800];
            int i;
            if (technos_video_hw == 1)
            {
                Array.Copy(Generic.spriteram, srcbyte, 0x800);
            }
            else
            {
                Array.Copy(ddragon_spriteram, 0x800, srcbyte, 0, 0x800);
            }
            for (i = 0; i < (64 * 5); i += 5)
            {
                int attr = srcbyte[i + 1];
                if ((attr & 0x80) != 0)
                {
                    int sx = 240 - srcbyte[i + 4] + ((attr & 2) << 7);
                    int sy = 232 - srcbyte[i + 0] + ((attr & 1) << 8);
                    int size = (attr & 0x30) >> 4;
                    int flipx = (attr & 8);
                    int flipy = (attr & 4);
                    int dx = -16, dy = -16;
                    int which;
                    int color;
                    if (technos_video_hw == 2)
                    {
                        color = (srcbyte[i + 2] >> 5);
                        which = srcbyte[i + 3] + ((srcbyte[i + 2] & 0x1f) << 8);
                    }
                    else
                    {
                        if (technos_video_hw == 1)
                        {
                            if ((sx < -7) && (sx > -16))
                            {
                                sx += 256;
                            }
                            if ((sy < -7) && (sy > -16))
                            {
                                sy += 256;
                            }
                        }
                        color = (srcbyte[i + 2] >> 4) & 0x07;
                        which = srcbyte[i + 3] + ((srcbyte[i + 2] & 0x0f) << 8);
                    }
                    if (Generic.flip_screen_get() != 0)
                    {
                        sx = 240 - sx;
                        sy = 256 - sy;
                        flipx = (flipx == 0 ? 1 : 0);
                        flipy = (flipy == 0 ? 1 : 0);
                        dx = -dx;
                        dy = -dy;
                    }
                    which &= ~size;
                    switch (size)
                    {
                        case 0:
                            Drawgfx.common_drawgfx_ddragon(gfx2rom, 0x10, 0x10, 0x10, gfxtotalelement, which, color, flipx, flipy, sx, sy, cliprect);
                            break;
                        case 1:
                            Drawgfx.common_drawgfx_ddragon(gfx2rom, 0x10, 0x10, 0x10, gfxtotalelement, which, color, flipx, flipy, sx, sy + dy, cliprect);
                            Drawgfx.common_drawgfx_ddragon(gfx2rom, 0x10, 0x10, 0x10, gfxtotalelement, which + 1, color, flipx, flipy, sx, sy, cliprect);
                            break;
                        case 2:
                            Drawgfx.common_drawgfx_ddragon(gfx2rom, 0x10, 0x10, 0x10, gfxtotalelement, which, color, flipx, flipy, sx + dx, sy, cliprect);
                            Drawgfx.common_drawgfx_ddragon(gfx2rom, 0x10, 0x10, 0x10, gfxtotalelement, which + 2, color, flipx, flipy, sx, sy, cliprect);
                            break;
                        case 3:
                            Drawgfx.common_drawgfx_ddragon(gfx2rom, 0x10, 0x10, 0x10, gfxtotalelement, which, color, flipx, flipy, sx + dx, sy + dy, cliprect);
                            Drawgfx.common_drawgfx_ddragon(gfx2rom, 0x10, 0x10, 0x10, gfxtotalelement, which + 1, color, flipx, flipy, sx + dx, sy, cliprect);
                            Drawgfx.common_drawgfx_ddragon(gfx2rom, 0x10, 0x10, 0x10, gfxtotalelement, which + 2, color, flipx, flipy, sx, sy + dy, cliprect);
                            Drawgfx.common_drawgfx_ddragon(gfx2rom, 0x10, 0x10, 0x10, gfxtotalelement, which + 3, color, flipx, flipy, sx, sy, cliprect);
                            break;
                    }
                }
            }
        }
        public static void video_update_ddragon()
        {
            int scrollx = ddragon_scrollx_hi + ddragon_scrollx_lo;
            int scrolly = ddragon_scrolly_hi + ddragon_scrolly_lo;
            bg_tilemap.tilemap_set_scrollx(0, scrollx);
            bg_tilemap.tilemap_set_scrolly(0, scrolly);
            bg_tilemap.tilemap_draw_primask(Video.new_clip, 0x10, 0);
            draw_sprites(Video.new_clip);
            fg_tilemap.tilemap_draw_primask(Video.new_clip, 0x10, 0);
        }
        public static void video_eof_ddragon()
        {

        }
    }
}
