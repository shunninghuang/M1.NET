using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    enum trans_t
    {
        WHOLLY_TRANSPARENT,
        WHOLLY_OPAQUE,
        MASKED
    }
    public struct RECT
    {
        public int min_x;
        public int min_y;
        public int max_x;
        public int max_y;
    }
    public partial class Tmap
    {
        public int rows;
        public int cols;
        public int tilewidth;
        public int tileheight;
        public int width;
        public int height;
        public int videoram_offset;
        public bool enable;
        public byte attributes;
        public bool all_tiles_dirty;
        public bool all_tiles_clean;
        public int palette_offset;
        public byte priority;
        public int scrollrows;
        public int scrollcols;
        public int[] rowscroll;
        public int[] colscroll;
        public int dx;
        public int dx_flipped;
        public int dy;
        public int dy_flipped;
        public ushort[] pixmap;
        public byte[,] flagsmap;
        public byte[] tileflags;
        public byte[,] pen_to_flags;
        public byte[] pen_data;
        public int mask, value;
        public int total_elements;
        public delegate int tilemap_mapper_func_delegate(int col, int row, int num_cols, int num_rows);
        public delegate void tile_get_info_func_delegate(int tile_index, int param);
        public tilemap_mapper_func_delegate mapper;
        public int[] memory_to_logical;
        public int max_logical_index;
        public int[] logical_to_memory;
        public int max_memory_index;
        //public tile_get_info_func_delegate tile_get_info;
        public int user_data;
        public Action<int, int, int> tile_update3;
        public Action<RECT, int, int> tilemap_draw_instance3;
        public int effective_rowscroll(int index)
        {
            int value;
            if ((attributes & Tilemap.TILEMAP_FLIPY) != 0)
            {
                index = scrollrows - 1 - index;
            }
            if ((attributes & Tilemap.TILEMAP_FLIPX) == 0)
            {
                value = dx - rowscroll[index];
            }
            else
            {
                value = Tilemap.screen_width - width - (dx_flipped - rowscroll[index]);
            }
            if (value < 0)
            {
                value = width - (-value) % width;
            }
            else
            {
                value %= width;
            }
            return value;
        }
        public int effective_colscroll(int index)
        {
            int value;
            if ((attributes & Tilemap.TILEMAP_FLIPX) != 0)
            {
                index = scrollcols - 1 - index;
            }
            if ((attributes & Tilemap.TILEMAP_FLIPY) == 0)
            {
                value = dy - colscroll[index];
            }
            else
            {
                value = Tilemap.screen_height - height - (dy_flipped - colscroll[index]);
            }
            if (value < 0)
            {
                value = height - (-value) % height;
            }
            else
            {
                value %= height;
            }
            return value;
        }
        public static Tmap tilemap_create(tilemap_mapper_func_delegate mapper, int tilewidth, int tileheight, int cols, int rows)
        {
            Tmap t1 = new Tmap();
            t1.rows = rows;
            t1.cols = cols;
            t1.tilewidth = tilewidth;
            t1.tileheight = tileheight;
            t1.width = cols * tilewidth;
            t1.height = rows * tileheight;
            t1.mapper = mapper;
            t1.mappings_create();
            t1.enable = true;
            t1.all_tiles_dirty = true;
            t1.scrollrows = 1;
            t1.scrollcols = 1;
            t1.rowscroll = new int[t1.height];
            t1.colscroll = new int[t1.width];
            t1.pixmap = new ushort[t1.width * t1.height];
            t1.pen_data = new byte[t1.tilewidth * t1.tileheight];
            t1.tileflags = new byte[t1.max_logical_index];
            t1.flagsmap = new byte[t1.height, t1.width];
            return t1;
        }
        public void tilemap_set_palette_offset(int offset)
        {
            palette_offset = offset;
        }
        public void tilemap_set_enable(bool _enable)
        {
            enable = _enable;
        }
        public static void tilemap_set_flip(Tmap tmap, byte _attributes)
        {
            if (tmap == null)
            {
                foreach (Tmap t1 in Tilemap.lsTmap)
                {
                    if (t1.attributes != _attributes)
                    {
                        t1.attributes = _attributes;
                        t1.mappings_update();
                    }
                }
            }
            else if (tmap.attributes != _attributes)
            {
                tmap.attributes = _attributes;
                tmap.mappings_update();
            }
        }
        public void get_row_col(int index, out int row, out int col)
        {
            if ((attributes & Tilemap.TILEMAP_FLIPX) != 0)
            {
                col = cols - 1 - index % cols;
            }
            else
            {
                col = index % cols;
            }
            if ((attributes & Tilemap.TILEMAP_FLIPY) != 0)
            {
                row = rows - 1 - index / cols;
            }
            else
            {
                row = index / cols;
            }
        }
        public void tilemap_mark_tile_dirty(int memindex)
        {
            if (memindex < max_memory_index)
            {
                int logindex = memory_to_logical[memindex];
                if (logindex != -1)
                {
                    tileflags[logindex] = Tilemap.TILE_FLAG_DIRTY;
                    all_tiles_clean = false;
                }
            }
        }
        public static void tilemap_mark_all_tiles_dirty(Tmap tmap)
        {
            if (tmap == null)
            {
                foreach (Tmap t1 in Tilemap.lsTmap)
                {
                    tilemap_mark_all_tiles_dirty(t1);
                }
            }
            else
            {
                tmap.all_tiles_dirty = true;
                tmap.all_tiles_clean = false;
            }
        }
        public void tilemap_set_scroll_rows(int scroll_rows)
        {
            scrollrows = scroll_rows;
        }
        public void tilemap_set_scroll_cols(int scroll_cols)
        {
            scrollcols = scroll_cols;
        }
        public void tilemap_set_scrolldx(int _dx,int _dx2)
        {
            dx = _dx;
            dx_flipped = _dx2;
        }
        public void tilemap_set_scrolldy(int _dy,int _dy2)
        {
            dy = _dy;
            dy_flipped = _dy2;
        }
        public void tilemap_set_scrollx(int which, int value)
        {
            if (which < scrollrows)
            {
                rowscroll[which] = value;
            }
        }
        public void tilemap_set_scrolly(int which, int value)
        {
            if (which < scrollcols)
            {
                colscroll[which] = value;
            }
        }
        public RECT sect_rect(RECT dst, RECT src)
        {
            RECT dst2 = dst;
            if (src.min_x > dst.min_x)
            {
                dst2.min_x = src.min_x;
            }
            if (src.max_x < dst.max_x)
            {
                dst2.max_x = src.max_x;
            }
            if (src.min_y > dst.min_y)
            {
                dst2.min_y = src.min_y;
            }
            if (src.max_y < dst.max_y)
            {
                dst2.max_y = src.max_y;
            }
            return dst2;
        }
        public void tilemap_draw_primask(RECT cliprect, int flags, byte _priority)
        {
            int xpos, ypos;
            if (!enable)
            {
                return;
            }
            mask = 0x0f | flags;
            value = flags;
            priority = _priority;
            if (all_tiles_dirty)
            {
                Array.Copy(Tilemap.bbFF, tileflags, cols * rows);
                all_tiles_dirty = false;
            }
            if (scrollrows == 1 && scrollcols == 1)
            {
                int scrollx = effective_rowscroll(0);
                int scrolly = effective_colscroll(0);
                for (ypos = scrolly - height; ypos <= cliprect.max_y; ypos += height)
                {
                    for (xpos = scrollx - width; xpos <= cliprect.max_x; xpos += width)
                    {
                        tilemap_draw_instance3(cliprect, xpos, ypos);
                    }
                }
            }
            else if (scrollcols == 1)
            {
                RECT rect = cliprect;
                int rowheight = height / scrollrows;
                int scrolly = effective_colscroll(0);
                int currow, nextrow;
                for (ypos = scrolly - height; ypos <= cliprect.max_y; ypos += height)
                {
                    int firstrow = Math.Max((cliprect.min_y - ypos) / rowheight, 0);
                    int lastrow = Math.Min((cliprect.max_y - ypos) / rowheight, scrollrows - 1);
                    for (currow = firstrow; currow <= lastrow; currow = nextrow)
                    {
                        int scrollx = effective_rowscroll(currow);
                        for (nextrow = currow + 1; nextrow <= lastrow; nextrow++)
                        {
                            if (effective_rowscroll(nextrow) != scrollx)
                            {
                                break;
                            }
                        }
                        rect.min_y = currow * rowheight + ypos;
                        rect.max_y = nextrow * rowheight - 1 + ypos;
                        rect = sect_rect(rect, cliprect);
                        for (xpos = scrollx - width; xpos <= cliprect.max_x; xpos += width)
                        {
                            tilemap_draw_instance3(rect, xpos, ypos);
                        }
                    }
                }
            }
            else if (scrollrows == 1)
            {
                int i1 = 1;
            }
        }
        public static int tilemap_scan_rows(int col, int row, int num_cols, int num_rows)
        {
            return row * num_cols + col;
        }
        public static int tilemap_scan_cols(int col, int row, int num_cols, int num_rows)
        {
            return col * num_rows + row;
        }
        public void mappings_create()
        {
            int col, row;
            max_logical_index = rows * cols;
            max_memory_index = 0;
            for (row = 0; row < rows; row++)
            {
                for (col = 0; col < cols; col++)
                {
                    int memindex = mapper(col, row, cols, rows);
                    max_memory_index = Math.Max(max_memory_index, memindex);
                }
            }
            max_memory_index++;
            memory_to_logical = new int[max_memory_index];
            logical_to_memory = new int[max_logical_index];
            mappings_update();
        }
        public void mappings_update()
        {
            int logindex;
            int memindex;
            for (memindex = 0; memindex < max_memory_index; memindex++)
            {
                memory_to_logical[memindex] = -1;
            }
            for (logindex = 0; logindex < max_logical_index; logindex++)
            {
                int logical_col = logindex % cols;
                int logical_row = logindex / cols;
                memindex = mapper(logical_col, logical_row, cols, rows);
                int flipped_logindex;
                if ((attributes & Tilemap.TILEMAP_FLIPX) != 0)
                {
                    logical_col = (cols - 1) - logical_col;
                }
                if ((attributes & Tilemap.TILEMAP_FLIPY) != 0)
                {
                    logical_row = (rows - 1) - logical_row;
                }
                flipped_logindex = logical_row * cols + logical_col;
                memory_to_logical[memindex] = flipped_logindex;
                logical_to_memory[flipped_logindex] = memindex;
            }
            tilemap_mark_all_tiles_dirty(this);
        }
        public byte tile_draw(byte[] bb1, int pen_data_offset, int x0, int y0, int palette_base,byte category, byte group, byte flags)
        {
            byte andmask = 0xff, ormask = 0;
            int dx0 = 1, dy0 = 1;
            int tx, ty;
            byte pen, map;
            int offset1 = 0;
            int offsety1;
            int xoffs;
            Array.Copy(bb1, pen_data_offset, pen_data, 0, tilewidth * tileheight);
            if ((flags & Tilemap.TILE_FLIPY) != 0)
            {
                y0 += tileheight - 1;
                dy0 = -1;
            }
            if ((flags & Tilemap.TILE_FLIPX) != 0)
            {
                x0 += tilewidth - 1;
                dx0 = -1;
            }
            for (ty = 0; ty < tileheight; ty++)
            {
                xoffs = 0;
                offsety1 = y0;
                y0 += dy0;
                for (tx = 0; tx < tilewidth; tx++)
                {
                    pen = pen_data[offset1];
                    map = pen_to_flags[group, pen];
                    offset1++;
                    pixmap[offsety1 * width + x0 + xoffs] = (ushort)(palette_base + pen);
                    flagsmap[offsety1, x0 + xoffs] = (byte)(map | category);
                    andmask &= map;
                    ormask |= map;
                    xoffs += dx0;
                }
            }
            return (byte)(andmask ^ ormask);
        }
        public byte tile_apply_bitmask(byte[] bb1, int maskdata_offset, int x0, int y0, byte category, byte flags)
        {
            byte andmask = 0xff, ormask = 0;
            int dx0 = 1, dy0 = 1;
            int bitoffs = 0;
            int tx, ty;
            int offsety1;
            if ((flags & Tilemap.TILE_FLIPY) != 0)
            {
                y0 += tileheight - 1;
                dy0 = -1;
            }
            if ((flags & Tilemap.TILE_FLIPX) != 0)
            {
                x0 += tilewidth - 1;
                dx0 = -1;
            }
            for (ty = 0; ty < tileheight; ty++)
            {
                int xoffs = 0;
                offsety1 = y0;
                y0 += dy0;
                for (tx = 0; tx < tilewidth; tx++)
                {
                    byte map = flagsmap[offsety1, x0 + xoffs];
                    if ((bb1[maskdata_offset + bitoffs / 8] & (0x80 >> (bitoffs & 7))) == 0)
                    {
                        map = flagsmap[offsety1, x0 + xoffs] = category;
                    }
                    andmask &= map;
                    ormask |= map;
                    xoffs += dx0;
                    bitoffs++;
                }
            }
            return (byte)(andmask ^ ormask);
        }
    }
    public class Tilemap
    {
        public static List<Tmap> lsTmap = new List<Tmap>();
        public static byte[,] priority_bitmap;
        public static byte[,] bb00;
        public static byte[] bb0F,bbFF;
        public static int screen_width, screen_height;
        private static int INVALID_LOGICAL_INDEX = -1;
        public static byte TILEMAP_PIXEL_TRANSPARENT = 0x00;
        private static byte TILEMAP_PIXEL_CATEGORY_MASK = 0x0f;		/* category is stored in the low 4 bits */
        public static byte TILE_FLAG_DIRTY = 0xff;
        public static byte TILEMAP_DRAW_OPAQUE = 0x80;
        public static byte TILEMAP_PIXEL_LAYER0 = 0x10;
        public static byte TILE_FLIPX = 0x01;		/* draw this tile horizontally flipped */
        public static byte TILE_FLIPY = 0x02;		/* draw this tile vertically flipped */
        public static byte TILEMAP_FLIPX = TILE_FLIPX;	/* draw the tilemap horizontally flipped */
        public static byte TILEMAP_FLIPY = TILE_FLIPY;	/* draw the tilemap vertically flipped */
        public static void tilemap_init()
        {
            int i, j;
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                case "CPS2turbo":
                    screen_width = 0x200;
                    screen_height = 0x200;
                    priority_bitmap = new byte[0x200, 0x200];
                    break;
                case "Data East":
                    screen_width = 0x100;
                    screen_height = 0x100;
                    break;
                case "Tehkan":
                    screen_width = 0x100;
                    screen_height = 0x100;
                    break;
                case "Technos":
                    screen_width = 0x180;
                    screen_height = 0x110;
                    break;
                case "Tad":
                    screen_width = 0x100;
                    screen_height = 0x100;
                    break;
                case "Megasys1":
                    screen_width = 0x196;
                    screen_height = 0x107;
                    priority_bitmap = new byte[0x107, 0x196];
                    break;
                case "Gaelco":
                    screen_width = 0x200;
                    screen_height = 0x200;
                    priority_bitmap = new byte[0x200, 0x200];
                    break;
                case "Namco System 1":
                    screen_width = 0x200;
                    screen_height = 0x200;
                    priority_bitmap = new byte[0x200, 0x200];
                    break;
                case "PGM":
                    screen_width = 0x200;
                    screen_height = 0x200;
                    priority_bitmap = new byte[0x200, 0x200];
                    break;
                case "M72":
                    screen_width = 0x200;
                    screen_height = 0x200;
                    priority_bitmap = new byte[0x200, 0x200];
                    break;
                case "M92":
                    screen_width = 0x200;
                    screen_height = 0x100;
                    priority_bitmap = new byte[0x200, 0x200];
                    break;
                case "Taito":
                    screen_width = 0x140;
                    screen_height = 0x100;
                    priority_bitmap = new byte[0x100, 0x140];
                    Taito.tilemap_init();
                    break;
                case "Taito B":
                    screen_width = 0x200;
                    screen_height = 0x100;
                    priority_bitmap = new byte[0x100, 0x200];
                    break;
                case "Konami 68000":
                    screen_width = 0x200;
                    screen_height = 0x200;
                    priority_bitmap = new byte[0x200, 0x200];
                    break;
                case "Capcom":
                    screen_width = 0x200;
                    screen_height = 0x200;
                    priority_bitmap = new byte[0x200, 0x200];
                    break;
            }            
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                case "CPS2turbo":
                case "Data East":
                case "Tehkan":
                case "Technos":
                case "Tad":                
                case "Gaelco":
                case "Namco System 1":
                case "PGM":
                case "M72":
                case "Taito":
                case "Taito B":
                case "Konami 68000":
                    bb0F = new byte[0x400];
                    bbFF = new byte[0x2000];
                    for (i = 0; i < 0x2000; i++)
                    {
                        bbFF[i] = 0xff;
                    }
                    for (i = 0; i < 0x400; i++)
                    {
                        bb0F[i] = 0x0f;
                    }
                    break;
                case "Megasys1":
                    bbFF = new byte[0x200000];
                    for (i = 0; i < 0x200000; i++)
                    {
                        bbFF[i] = 0xff;
                    }
                    break;
                case "M92":
                    bbFF = new byte[0x2000];
                    for (i = 0; i < 0x2000; i++)
                    {
                        bbFF[i] = 0xff;
                    }
                    bb00 = new byte[0x200, 0x200];
                    for (i = 0; i < 0x200; i++)
                    {
                        for (j = 0; j < 0x200; j++)
                        {
                            bb00[i, j] = 0;
                        }
                    }
                    break;
                case "Capcom":
                    bbFF = new byte[0x8000];
                    for (i = 0; i < 0x8000; i++)
                    {
                        bbFF[i] = 0xff;
                    }
                    break;
            }
        }
    }
}