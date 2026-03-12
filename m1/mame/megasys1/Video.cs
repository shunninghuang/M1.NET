using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Megasys1
    {
        public static int[] megasys1_layers_order;
        public static void video_start_megasys1()
        {
            int i, j;
            megasys1_tmap = new Tmap[3];
            megasys1_tilemap = new Tmap[3][][];
            for (i = 0; i < 3; i++)
            {
                megasys1_tilemap[i] = new Tmap[2][];
                for (j = 0; j < 2; j++)
                {
                    megasys1_tilemap[i][j] = new Tmap[4];
                }
            }
            create_tilemaps();
            megasys1_tmap[0] = megasys1_tilemap[0][0][0];
            megasys1_tmap[1] = megasys1_tilemap[1][0][0];
            megasys1_tmap[2] = megasys1_tilemap[2][0][0];
            megasys1_active_layers = megasys1_sprite_bank = megasys1_screen_flag = megasys1_sprite_flag = 0;
            for (i = 0; i < 3; i++)
            {
                megasys1_scroll_flag[i] = megasys1_scrollx[i] = megasys1_scrolly[i] = 0;
            }
            megasys1_bits_per_color_code = 4;
            megasys1_8x8_scroll_factor[0] = 1; megasys1_16x16_scroll_factor[0] = 4;
            megasys1_8x8_scroll_factor[1] = 1; megasys1_16x16_scroll_factor[1] = 4;
            megasys1_8x8_scroll_factor[2] = 1; megasys1_16x16_scroll_factor[2] = 4;
            if (Machine.sName == "soldmj")
            {
                megasys1_8x8_scroll_factor[1] = 4;
                megasys1_16x16_scroll_factor[1] = 4;
            }
            hardware_type_z = 0;
            if (Machine.sName == "lomakai" || Machine.sName == "makaiden")
            {
                hardware_type_z = 1;
            }
            megasys1_layers_order = new int[0x10];
        }
        public static void scrollram_w(int which, int offset, ushort data)
        {
            megasys1_scrollram[which][offset] = data;
            if (offset < 0x40000 / 2 && megasys1_tmap[which] != null)
            {
                if ((megasys1_scroll_flag[which] & 0x10) != 0)
                {
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset);
                }
                else
                {
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset * 4 + 0);
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset * 4 + 1);
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset * 4 + 2);
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset * 4 + 3);
                }
            }
        }
        public static void scrollram_w1(int which, int offset, byte data)
        {
            megasys1_scrollram[which][offset] = (ushort)((data << 8) | (megasys1_scrollram[which][offset] & 0xff));
            if (offset < 0x40000 / 2 && megasys1_tmap[which] != null)
            {
                if ((megasys1_scroll_flag[which] & 0x10) != 0)
                {
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset);
                }
                else
                {
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset * 4 + 0);
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset * 4 + 1);
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset * 4 + 2);
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset * 4 + 3);
                }
            }
        }
        public static void scrollram_w2(int which, int offset, byte data)
        {
            megasys1_scrollram[which][offset] = (ushort)((megasys1_scrollram[which][offset] & 0xff00) | data);
            if (offset < 0x40000 / 2 && megasys1_tmap[which] != null)
            {
                if ((megasys1_scroll_flag[which] & 0x10) != 0)
                {
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset);
                }
                else
                {
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset * 4 + 0);
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset * 4 + 1);
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset * 4 + 2);
                    megasys1_tmap[which].tilemap_mark_tile_dirty(offset * 4 + 3);
                }
            }
        }
        public static void megasys1_scrollram_0_w(int offset, ushort data)
        {
            scrollram_w(0, offset, data);
        }
        public static void megasys1_scrollram_1_w(int offset, ushort data)
        {
            scrollram_w(1, offset, data);
        }
        public static void megasys1_scrollram_2_w(int offset, ushort data)
        {
            scrollram_w(2, offset, data);
        }
        public static int megasys1_scan_8x8(int col, int row, int num_cols, int num_rows)
        {
            return (col * 0x20) + (row / 0x20) * 0x400 * (num_cols / 0x20) + (row % 0x20);
        }
        public static int megasys1_scan_16x16(int col, int row, int num_cols, int num_rows)
        {
            return (((col / 2) * (0x20 / 2)) + ((row / 2) / (0x20 / 2)) * (0x400 / 4) * (num_cols / 0x20) + ((row / 2) % (0x20 / 2))) * 4 + (row & 1) + (col & 1) * 2;
        }
        public static void create_tilemaps()
        {
            int layer, i, j;
            for (layer = 0; layer < 3; layer++)
            {
                megasys1_tilemap[layer][0][0] = Tmap.tilemap_create(megasys1_scan_16x16, 8, 8, 0x20 * 16, 0x20 * 2);
                megasys1_tilemap[layer][0][1] = Tmap.tilemap_create(megasys1_scan_16x16, 8, 8, 0x20 * 8, 0x20 * 4);
                megasys1_tilemap[layer][0][2] = Tmap.tilemap_create(megasys1_scan_16x16, 8, 8, 0x20 * 4, 0x20 * 8);
                megasys1_tilemap[layer][0][3] = Tmap.tilemap_create(megasys1_scan_16x16, 8, 8, 0x20 * 2, 0x20 * 16);
                megasys1_tilemap[layer][1][0] = Tmap.tilemap_create(megasys1_scan_8x8, 8, 8, 0x20 * 8, 0x20 * 1);
                megasys1_tilemap[layer][1][1] = Tmap.tilemap_create(megasys1_scan_8x8, 8, 8, 0x20 * 4, 0x20 * 2);
                megasys1_tilemap[layer][1][2] = Tmap.tilemap_create(megasys1_scan_8x8, 8, 8, 0x20 * 4, 0x20 * 2);
                megasys1_tilemap[layer][1][3] = Tmap.tilemap_create(megasys1_scan_8x8, 8, 8, 0x20 * 2, 0x20 * 4);
                for (i = 0; i < 8; i++)
                {
                    megasys1_tilemap[layer][i / 4][i % 4].user_data = layer;
                    megasys1_tilemap[layer][i / 4][i % 4].pen_to_flags = new byte[1, 16];
                    for (j = 0; j < 15; j++)
                    {
                        megasys1_tilemap[layer][i / 4][i % 4].pen_to_flags[0, j] = 0x10;
                    }
                    megasys1_tilemap[layer][i / 4][i % 4].pen_to_flags[0, 15] = 0;
                }
            }
            for (layer = 0; layer < 3; layer++)
            {
                for (j = 0; j < 4; j++)
                {
                    megasys1_tilemap[layer][0][j].tile_update3 = megasys1_tilemap[layer][0][j].tile_update_0;
                    megasys1_tilemap[layer][1][j].tile_update3 = megasys1_tilemap[layer][1][j].tile_update_1;
                    switch (Machine.sName)
                    {
                        case "lomakai":
                        case "makaiden":
                            megasys1_tilemap[layer][1][j].tile_update3 = megasys1_tilemap[layer][1][j].tile_update_1_lomakai;
                            break;
                    }
                }
            }
            for (layer = 0; layer < 3; layer++)
            {
                for (i = 0; i < 2; i++)
                {
                    for (j = 0; j < 4; j++)
                    {
                        megasys1_tilemap[layer][i][j].tilemap_draw_instance3 = megasys1_tilemap[layer][i][j].tilemap_draw_instance_cps;
                    }
                }
            }
        }
        public static void megasys1_set_vreg_flag(int which, ushort data)
        {
            if (megasys1_scroll_flag[which] != data)
            {
                megasys1_scroll_flag[which] = data;
                megasys1_tmap[which] = megasys1_tilemap[which][(data >> 4) & 1][data & 3];
                megasys1_tmap[which].all_tiles_dirty = true;
            }
        }
        public static void megasys1_set_vreg_flag1(int which, byte data)
        {
            ushort u1 = (ushort)((data << 8) | (megasys1_scroll_flag[which] & 0xff));
            if (megasys1_scroll_flag[which] != u1)
            {
                megasys1_scroll_flag[which] = u1;
                megasys1_tmap[which] = megasys1_tilemap[which][(u1 >> 4) & 1][u1 & 3];
                megasys1_tmap[which].all_tiles_dirty = true;
            }
        }
        public static void megasys1_set_vreg_flag2(int which, byte data)
        {
            ushort u1 = (ushort)((megasys1_scroll_flag[which] & 0xff00) | data);
            if (megasys1_scroll_flag[which] != u1)
            {
                megasys1_scroll_flag[which] = u1;
                megasys1_tmap[which] = megasys1_tilemap[which][(u1 >> 4) & 1][u1 & 3];
                megasys1_tmap[which].all_tiles_dirty = true;
            }
        }
        public static void active_layers_w(ushort data)
        {
            megasys1_active_layers = data;
            Video.video_screen_update_partial(Video.video_screen_get_vpos());
        }
        public static void active_layers_w1(byte data)
        {
            megasys1_active_layers = (ushort)((data << 8) | (megasys1_active_layers & 0xff));
            Video.video_screen_update_partial(Video.video_screen_get_vpos());
        }
        public static void active_layers_w2(byte data)
        {
            megasys1_active_layers = (ushort)((megasys1_active_layers & 0xff00) | data);
            Video.video_screen_update_partial(Video.video_screen_get_vpos());
        }
        public static void sprite_bank_w(ushort data)
        {
            megasys1_sprite_bank = data;
        }
        public static void sprite_bank_w1(byte data)
        {
            megasys1_sprite_bank = (ushort)((data << 8) | (megasys1_sprite_bank & 0xff));
        }
        public static void sprite_bank_w2(byte data)
        {
            megasys1_sprite_bank = (ushort)((megasys1_sprite_bank & 0xff00) | data);
        }
        public static void megasys1_scrollx0_w(ushort data)
        {
            megasys1_scrollx[0] = data;
        }
        public static void megasys1_scrollx0_w1(byte data)
        {
            megasys1_scrollx[0] = (ushort)((data << 8) | (megasys1_scrollx[0] & 0xff));
        }
        public static void megasys1_scrollx0_w2(byte data)
        {
            megasys1_scrollx[0] = (ushort)((megasys1_scrollx[0] & 0xff00) | data);
        }
        public static void megasys1_scrolly0_w(ushort data)
        {
            megasys1_scrolly[0] = data;
        }
        public static void megasys1_scrolly0_w1(byte data)
        {
            megasys1_scrolly[0] = (ushort)((data << 8) | (megasys1_scrolly[0] & 0xff));
        }
        public static void megasys1_scrolly0_w2(byte data)
        {
            megasys1_scrolly[0] = (ushort)((megasys1_scrolly[0] & 0xff00) | data);
        }
        public static void megasys1_scrollx1_w(ushort data)
        {
            megasys1_scrollx[1] = data;
        }
        public static void megasys1_scrollx1_w1(byte data)
        {
            megasys1_scrollx[1] = (ushort)((data << 8) | (megasys1_scrollx[1] & 0xff));
        }
        public static void megasys1_scrollx1_w2(byte data)
        {
            megasys1_scrollx[1] = (ushort)((megasys1_scrollx[1] & 0xff00) | data);
        }
        public static void megasys1_scrolly1_w(ushort data)
        {
            megasys1_scrolly[1] = data;
        }
        public static void megasys1_scrolly1_w1(byte data)
        {
            megasys1_scrolly[1] = (ushort)((data << 8) | (megasys1_scrolly[1] & 0xff));
        }
        public static void megasys1_scrolly1_w2(byte data)
        {
            megasys1_scrolly[1] = (ushort)((megasys1_scrolly[1] & 0xff00) | data);
        }
        public static void megasys1_scrollx2_w(ushort data)
        {
            megasys1_scrollx[2] = data;
        }
        public static void megasys1_scrollx2_w1(byte data)
        {
            megasys1_scrollx[2] = (ushort)((data << 8) | (megasys1_scrollx[2] & 0xff));
        }
        public static void megasys1_scrollx2_w2(byte data)
        {
            megasys1_scrollx[2] = (ushort)((megasys1_scrollx[2] & 0xff00) | data);
        }
        public static void megasys1_scrolly2_w(ushort data)
        {
            megasys1_scrolly[2] = data;
        }
        public static void megasys1_scrolly2_w1(byte data)
        {
            megasys1_scrolly[2] = (ushort)((data << 8) | (megasys1_scrolly[2] & 0xff));
        }
        public static void megasys1_scrolly2_w2(byte data)
        {
            megasys1_scrolly[2] = (ushort)((megasys1_scrolly[2] & 0xff00) | data);
        }
        public static ushort sprite_flag_r()
        {
            return megasys1_sprite_flag;
        }
        public static void sprite_flag_w(ushort data)
        {
            megasys1_sprite_flag = data;
        }
        public static void sprite_flag_w1(byte data)
        {
            megasys1_sprite_flag = (ushort)((data << 8) | (megasys1_sprite_flag & 0xff));
        }
        public static void sprite_flag_w2(byte data)
        {
            megasys1_sprite_flag = (ushort)((megasys1_sprite_flag & 0xff00) | data);
        }
        public static void screen_flag_w(ushort data)
        {
            megasys1_screen_flag = data;
            if ((megasys1_screen_flag & 0x10) != 0)
            {
                Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_RESET, LineState.ASSERT_LINE);
            }
            else
            {
                Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_RESET, LineState.CLEAR_LINE);
            }
        }
        public static void screen_flag_w1(byte data)
        {
            megasys1_screen_flag = (ushort)((data << 8) | (megasys1_screen_flag & 0xff));
            if ((megasys1_screen_flag & 0x10) != 0)
            {
                Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_RESET, LineState.ASSERT_LINE);
            }
            else
            {
                Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_RESET, LineState.CLEAR_LINE);
            }
        }
        public static void screen_flag_w2(byte data)
        {
            megasys1_screen_flag = (ushort)((megasys1_screen_flag & 0xff00) | data);
            if ((megasys1_screen_flag & 0x10) != 0)
            {
                Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_RESET, LineState.ASSERT_LINE);
            }
            else
            {
                Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_RESET, LineState.CLEAR_LINE);
            }
        }
        public static void screen_flag_wd(ushort data)
        {
            megasys1_screen_flag = data;
        }
        public static void screen_flag_wd1(byte data)
        {
            megasys1_screen_flag = (ushort)((data << 8) | (megasys1_screen_flag & 0xff));
        }
        public static void screen_flag_wd2(byte data)
        {
            megasys1_screen_flag = (ushort)((megasys1_screen_flag & 0xff00) | data); ;
        }
        public static void megasys1_soundlatch_w(ushort data)
        {
            Sound.soundlatch_w(data);
            Cpuint.cpunum_set_input_line(1, 4, LineState.HOLD_LINE);
        }
        public static void megasys1_soundlatch_w2(byte data)
        {
            Sound.soundlatch_w(data);
            Cpuint.cpunum_set_input_line(1, 4, LineState.HOLD_LINE);
        }
        public static void soundlatch_z_w(ushort data)
        {
            Sound.soundlatch_w((ushort)(data & 0xff));
            Cpuint.cpunum_set_input_line(1, 5, LineState.HOLD_LINE);
        }
        public static void soundlatch_z_w2(byte data)
        {
            Sound.soundlatch_w(data);
            Cpuint.cpunum_set_input_line(1, 5, LineState.HOLD_LINE);
        }
        public static void soundlatch_c_w(ushort data)
        {
            Sound.soundlatch_w(data);
            Cpuint.cpunum_set_input_line(1, 2, LineState.HOLD_LINE);
        }
        public static void soundlatch_c_w2(byte data)
        {
            Sound.soundlatch_w(data);
            Cpuint.cpunum_set_input_line(1, 2, LineState.HOLD_LINE);
        }
        public static void monkelf_scroll0_w(int offset, ushort data)
        {
            if (offset == 0)
            {
                data = (ushort)(data - (((data & 0x0f) > 0x0d) ? 0x10 : 0));
            }
            if (offset == 0)
            {
                megasys1_scrollx0_w(data);
            }
            else if (offset == 1)
            {
                megasys1_scrolly0_w(data);
            }
            else if (offset == 2)
            {
                megasys1_set_vreg_flag(0, data);
            }
        }
        public static void monkelf_scroll1_w(int offset, ushort data)
        {
            if (offset == 0)
            {
                data = (ushort)(data - (((data & 0x0f) > 0x0b) ? 0x10 : 0));
            }
            if (offset == 0)
            {
                megasys1_scrollx1_w(data);
            }
            else if (offset == 1)
            {
                megasys1_scrolly1_w(data);
            }
            else if (offset == 2)
            {
                megasys1_set_vreg_flag(1, data);
            }
        }
        public static void draw_sprites(RECT cliprect)
        {
            int color, code, sx, sy, flipx, flipy, attr, sprite, offs, color_mask;
            int objectdata_offset, spritedata_offset;
            if (hardware_type_z == 0)
            {
                color_mask = (megasys1_sprite_flag & 0x100) != 0 ? 0x07 : 0x0f;
                for (offs = (0x800 - 8) / 2; offs >= 0; offs -= 8 / 2)
                {
                    for (sprite = 0; sprite < 4; sprite++)
                    {
                        objectdata_offset = offs + 0x400 * sprite;
                        spritedata_offset = 0x4000 + (megasys1_objectram[objectdata_offset] & 0x7f) * 8;
                        attr = Memory.mainram[mainram_offset + spritedata_offset * 2 + 8] * 0x100 + Memory.mainram[mainram_offset + spritedata_offset * 2 + 9];
                        if (((attr & 0xc0) >> 6) != sprite)
                        {
                            continue;
                        }
                        sx = (Memory.mainram[mainram_offset + spritedata_offset * 2 + 0x0a] * 0x100 + Memory.mainram[mainram_offset + spritedata_offset * 2 + 0x0b] + megasys1_objectram[objectdata_offset + 0x02 / 2]) % 0x200;
                        sy = (Memory.mainram[mainram_offset + spritedata_offset * 2 + 0x0c] * 0x100 + Memory.mainram[mainram_offset + spritedata_offset * 2 + 0x0d] + megasys1_objectram[objectdata_offset + 0x04 / 2]) % 0x200;
                        if (sx > 256 - 1)
                        {
                            sx -= 512;
                        }
                        if (sy > 256 - 1)
                        {
                            sy -= 512;
                        }
                        flipx = attr & 0x40;
                        flipy = attr & 0x80;
                        if ((megasys1_screen_flag & 1) != 0)
                        {
                            flipx = (flipx == 0 ? 1 : 0);
                            flipy = (flipy == 0 ? 1 : 0);
                            sx = 240 - sx;
                            sy = 240 - sy;
                        }
                        code = Memory.mainram[mainram_offset + spritedata_offset * 2 + 0x0e] * 0x100 + Memory.mainram[mainram_offset + spritedata_offset * 2 + 0x0f] + megasys1_objectram[objectdata_offset + 0x06 / 2];
                        color = (attr & color_mask);
                        Drawgfx.common_drawgfx_megasys1(spritesrom, 0x300, (code & 0xfff) + ((megasys1_sprite_bank & 1) << 12), color, flipx, flipy, sx, sy, cliprect, (uint)((((attr & 0x08) != 0) ? 0x0c : 0x0a) | (1 << 31)));
                    }
                }
            }
            else
            {
                for (sprite = 0x80 - 1; sprite >= 0; sprite--)
                {
                    spritedata_offset = 0x4000 + sprite * 8;
                    attr = Memory.mainram[mainram_offset + spritedata_offset * 2 + 8] * 0x100 + Memory.mainram[mainram_offset + spritedata_offset * 2 + 9];
                    sx = (Memory.mainram[mainram_offset + spritedata_offset * 2 + 0x0a] * 0x100 + Memory.mainram[mainram_offset + spritedata_offset * 2 + 0x0b]) % 0x200;
                    sy = (Memory.mainram[mainram_offset + spritedata_offset * 2 + 0x0c] * 0x100 + Memory.mainram[mainram_offset + spritedata_offset * 2 + 0x0d]) % 0x200;
                    if (sx > 256 - 1)
                    {
                        sx -= 512;
                    }
                    if (sy > 256 - 1)
                    {
                        sy -= 512;
                    }
                    code = Memory.mainram[mainram_offset + spritedata_offset * 2 + 0x0e] * 0x100 + Memory.mainram[mainram_offset + spritedata_offset * 2 + 0x0f];
                    color = (attr & 0x0F);
                    flipx = attr & 0x40;
                    flipy = attr & 0x80;
                    if ((megasys1_screen_flag & 1) != 0)
                    {
                        flipx = (flipx == 0 ? 1 : 0);
                        flipy = (flipy == 0 ? 1 : 0);
                        sx = 240 - sx;
                        sy = 240 - sy;
                    }
                    Drawgfx.common_drawgfx_megasys1(spritesrom, 0x100, code, color, flipx, flipy, sx, sy, cliprect, (uint)((((attr & 0x08) != 0) ? 0x0c : 0x0a) | (1 << 31)));
                }
            }
        }
        public static void palette_init_megasys1(byte[] color_prom)
        {
            int pri_code, offset, i, order;
            i = 0;
            for (pri_code = 0; pri_code < 0x10; pri_code++)
            {
                int[] layers_order = new int[2];
                for (offset = 0; offset < 2; offset++)
                {
                    int enable_mask = 0xf;
                    layers_order[offset] = 0xfffff;
                    do
                    {
                        int top = color_prom[pri_code * 0x20 + offset + enable_mask * 2] & 3;
                        int top_mask = 1 << top;
                        int result = 0;
                        for (i = 0; i < 0x10; i++)
                        {
                            int opacity = i & enable_mask;
                            int layer = color_prom[pri_code * 0x20 + offset + opacity * 2];
                            if (opacity != 0)
                            {
                                if ((opacity & top_mask) != 0)
                                {
                                    if (layer != top)
                                    {
                                        result |= 1;
                                    }
                                }
                                else
                                {
                                    if (layer == top)
                                    {
                                        result |= 2;
                                    }
                                    else
                                    {
                                        result |= 4;
                                    }
                                }
                            }
                        }
                        layers_order[offset] = ((layers_order[offset] << 4) | top) & 0xfffff;
                        enable_mask &= ~top_mask;
                        if ((result & 1) != 0)
                        {
                            layers_order[offset] = 0xfffff;
                            break;
                        }

                        if ((result & 6) == 6)
                        {
                            layers_order[offset] = 0xfffff;
                            break;
                        }
                        if (result == 2)
                        {
                            enable_mask = 0;
                        }
                    }
                    while (enable_mask != 0);
                }
                order = 0xfffff;
                for (i = 5; i > 0; )
                {
                    int layer;
                    int layer0 = layers_order[0] & 0x0f;
                    int layer1 = layers_order[1] & 0x0f;
                    if (layer0 != 3)
                    {
                        if (layer1 == 3)
                        {
                            layer = 4;
                            layers_order[0] <<= 4;
                        }
                        else
                        {
                            layer = layer0;
                            if (layer0 != layer1)
                            {
                                order = 0xfffff;
                                break;
                            }

                        }
                    }
                    else
                    {
                        if (layer1 == 3)
                        {
                            layer = 0x43;
                            order <<= 4;
                            i--;
                        }
                        else
                        {
                            layer = 3;
                            layers_order[1] <<= 4;
                        }
                    }
                    order = (order << 4) | layer;
                    i--;
                    layers_order[0] >>= 4;
                    layers_order[1] >>= 4;
                }
                megasys1_layers_order[pri_code] = order & 0xfffff;
            }
        }
        public static void video_update_megasys1()
        {
            int i, flag, flag2 = 0, pri, primask;
            int active_layers;
            if (hardware_type_z != 0)
            {
                active_layers = 0x000b;
                pri = 0x0314f;
            }
            else
            {
                int reallyactive = 0;
                pri = megasys1_layers_order[(megasys1_active_layers & 0x0f0f) >> 8];
                if (pri == 0xfffff)
                {
                    pri = 0x04132;
                }
                for (i = 0; i < 5; i++)
                {
                    reallyactive |= 1 << ((pri >> (4 * i)) & 0x0f);
                }
                active_layers = megasys1_active_layers & reallyactive;
                active_layers |= 1 << ((pri & 0xf0000) >> 16);
            }
            Tmap.tilemap_set_flip(null, (byte)((megasys1_screen_flag & 1) != 0 ? (Tilemap.TILEMAP_FLIPY | Tilemap.TILEMAP_FLIPX) : 0));
            for (i = 0; i < 3; i++)
            {
                if (megasys1_tmap[i] != null)
                {
                    megasys1_tmap[i].tilemap_set_enable((active_layers & (1 << i)) != 0);
                    megasys1_tmap[i].tilemap_set_scrollx(0, megasys1_scrollx[i]);
                    megasys1_tmap[i].tilemap_set_scrolly(0, megasys1_scrolly[i]);
                }
            }
            Array.Clear(Tilemap.priority_bitmap, 0, 0x1a11a);
            flag = Tilemap.TILEMAP_DRAW_OPAQUE;
            primask = 0;
            for (i = 0; i < 5; i++)
            {
                int layer = (pri & 0xf0000) >> 16;
                pri <<= 4;

                switch (layer)
                {
                    case 0:
                    case 1:
                    case 2:
                        if ((megasys1_tmap[layer]) != null && ((active_layers & (1 << layer)) != 0))
                        {
                            if (flag == 0x80)
                            {
                                flag2 = 0;
                            }
                            else if (flag == 0)
                            {
                                flag2 = 0x10;
                            }
                            //tilemap_draw(bitmap,cliprect,megasys1_tmap[layer],flag,primask);
                            megasys1_tmap[layer].tilemap_draw_primask(Video.new_clip, flag2, (byte)primask);
                            flag = 0;
                        }
                        break;
                    case 3:
                    case 4:
                        if (flag != 0)
                        {
                            flag = 0;
                            //fillbitmap(bitmap,0,cliprect);
                            Array.Clear(Video.bitmapbase[Video.curbitmap], 0, 0x1a11a);
                        }
                        if ((megasys1_sprite_flag & 0x100) != 0)
                        {
                            primask |= 1 << (layer - 3);
                        }
                        else if (layer == 3)
                        {
                            primask |= 3;
                        }
                        break;
                }
            }
            if ((active_layers & 0x08) != 0)
            {
                draw_sprites(Video.new_clip);
            }
        }
    }
}
