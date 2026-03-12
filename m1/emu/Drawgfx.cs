using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mame
{
    public partial class Drawgfx
    {
        public static int[] gfx_drawmode_table = new int[256];
        public static int afterdrawmask;
        public static int[][] shadow_table = new int[4][];
        public static int imode;
        public static int spritecount;
        public static void SaveStateBinary(BinaryWriter writer)
        {
            writer.Write(imode);
        }
        public static void LoadStateBinary(BinaryReader reader)
        {
            imode = reader.ReadInt32();
        }
    }
}
