using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cpu.m68000;
using cpu.z80;

namespace mame
{
    public partial class Tad
    {
        public static void SaveStateBinary_tad_toki(BinaryWriter writer)
        {
            int i;
            writer.Write(dsw0);
            writer.Write(basebanksnd);
            writer.Write(main2sub,0,2);
            writer.Write(sub2main,0,2);
            writer.Write(main2sub_pending);
            writer.Write(sub2main_pending);
            writer.Write(sound_cpu);
            writer.Write(irq1);
            writer.Write(irq2);
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0xd800);
            writer.Write(Memory.audioram, 0, 0x800);
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(toki_background1_videoram16[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(toki_background2_videoram16[i]);
            }
            for (i = 0; i < 0x30; i++)
            {
                writer.Write(toki_scrollram16[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.paletteram16[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.videoram16[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.spriteram16[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.buffered_spriteram16[i]);
            }
            MC68000.mm1[0].SaveStateBinary(writer);
            Z80A.zz1[0].SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            Video.SaveStateBinary(writer);
            writer.Write(Sound.last_update_second);
            Cpuexec.SaveStateBinary(writer);
            Timer.SaveStateBinary(writer);
            YM3812.SaveStateBinary(writer);
            OKI6295.oo1[0].SaveStateBinary(writer);
            writer.Write(Sound.ym3812stream.output_sampindex);
            writer.Write(Sound.ym3812stream.output_base_sampindex);
            writer.Write(OKI6295.oo1[0].OKI.stream.output_sampindex);
            writer.Write(OKI6295.oo1[0].OKI.stream.output_base_sampindex);
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_tad_toki(BinaryReader reader)
        {
            int i;
            dsw0 = reader.ReadUInt16();
            basebanksnd = reader.ReadInt32();
            main2sub = reader.ReadBytes(2);
            sub2main = reader.ReadBytes(2);
            main2sub_pending = reader.ReadInt32();
            sub2main_pending = reader.ReadInt32();
            sound_cpu = reader.ReadInt32();
            irq1 = reader.ReadInt32();
            irq2 = reader.ReadInt32();
            for (i = 0; i < 0x400; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0xd800);
            Memory.audioram = reader.ReadBytes(0x800);
            for (i = 0; i < 0x400; i++)
            {
                toki_background1_videoram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                toki_background2_videoram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x30; i++)
            {
                toki_scrollram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.paletteram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.videoram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.spriteram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.buffered_spriteram16[i] = reader.ReadUInt16();
            }
            MC68000.mm1[0].LoadStateBinary(reader);
            Z80A.zz1[0].LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();
            Cpuexec.LoadStateBinary(reader);
            Timer.LoadStateBinary(reader);
            YM3812.LoadStateBinary(reader);
            OKI6295.oo1[0].LoadStateBinary(reader);
            Sound.ym3812stream.output_sampindex = reader.ReadInt32();
            Sound.ym3812stream.output_base_sampindex = reader.ReadInt32();
            OKI6295.oo1[0].OKI.stream.output_sampindex = reader.ReadInt32();
            OKI6295.oo1[0].OKI.stream.output_base_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
        public static void SaveStateBinary_tad_tokib(BinaryWriter writer)
        {
            int i;
            writer.Write(dsw0);
            writer.Write(toggle);
            writer.Write(msm5205next);
            writer.Write(basebanksnd);
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0xe000);
            writer.Write(Memory.audioram, 0, 0x800);
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(toki_background1_videoram16[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(toki_background2_videoram16[i]);
            }
            for (i = 0; i < 0x30; i++)
            {
                writer.Write(toki_scrollram16[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.paletteram16[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.videoram16[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.spriteram16[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.buffered_spriteram16[i]);
            }
            MC68000.mm1[0].SaveStateBinary(writer);
            Z80A.zz1[0].SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            Video.SaveStateBinary(writer);
            writer.Write(Sound.last_update_second);
            Cpuexec.SaveStateBinary(writer);
            Timer.SaveStateBinary(writer);
            YM3812.SaveStateBinary(writer);
            MSM5205.mm1[0].SaveStateBinary(writer);
            writer.Write(Sound.ym3812stream.output_sampindex);
            writer.Write(Sound.ym3812stream.output_base_sampindex);
            writer.Write(MSM5205.mm1[0].voice.stream.output_sampindex);
            writer.Write(MSM5205.mm1[0].voice.stream.output_base_sampindex);
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_tad_tokib(BinaryReader reader)
        {
            int i;
            dsw0 = reader.ReadUInt16();
            toggle = reader.ReadInt32();
            msm5205next = reader.ReadInt32();
            basebanksnd = reader.ReadInt32();
            for (i = 0; i < 0x400; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0xe000);
            Memory.audioram = reader.ReadBytes(0x800);
            for (i = 0; i < 0x400; i++)
            {
                toki_background1_videoram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                toki_background2_videoram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x30; i++)
            {
                toki_scrollram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.paletteram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.videoram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.spriteram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.buffered_spriteram16[i] = reader.ReadUInt16();
            }
            MC68000.mm1[0].LoadStateBinary(reader);
            Z80A.zz1[0].LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();
            Cpuexec.LoadStateBinary(reader);
            Timer.LoadStateBinary(reader);
            YM3812.LoadStateBinary(reader);
            MSM5205.mm1[0].LoadStateBinary(reader);
            Sound.ym3812stream.output_sampindex = reader.ReadInt32();
            Sound.ym3812stream.output_base_sampindex = reader.ReadInt32();
            MSM5205.mm1[0].voice.stream.output_sampindex = reader.ReadInt32();
            MSM5205.mm1[0].voice.stream.output_base_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
    }
}
