﻿using System;
using System.Collections.Generic;

namespace Files.Gm1Converter
{
    public class GM1FileHeader
    {
        #region Public

        /// <summary>
        /// The header has a length of 88-bytes, composed of 22 unsigned 32-bit integers. 
        /// </summary>
        public const int ByteSize = 88;

        /// <summary>
        /// Data type is and ID that represents what kind of images are stored, they are as follows:
        /// <para>1 – Interface items and some building animations. Images are stored similar to TGX images.</para>
        /// <para>2 – Animations.</para>
        /// <para>3 – Buildings. Images are stored similar to TGX images but with a Tile object.</para>
        /// <para>4 – Font. TGX format.</para>
        /// <para>5 and 7 – Walls, grass, stones and other. No compression, stored with 2-bytes per pixel.</para>
        /// </summary>
        public enum DataType : uint { Interface = 1, Animations = 2, TilesObject = 3, Font = 4, NOCompression = 5, TGXConstSize = 6, NOCompression1 = 7 };

        #endregion
        
        #region Variables

        private string name;

        private uint iUnknown1;
        private uint iUnknown2;
        private uint iUnknown3;
        private uint iNumberOfPictureinFile;
        private uint iUnknown4;
        private uint iDataType;
        private uint[] iUnknown5 = new uint[14];
        private uint iDataSize;
        private uint iUnknown6;
        
        private uint[] size = new uint[2];

        #endregion

        #region Construtor

        public GM1FileHeader(byte[] byteArray)
        {
            iUnknown1 = BitConverter.ToUInt32(byteArray, 0);
            iUnknown2 = BitConverter.ToUInt32(byteArray, 4);
            iUnknown3 = BitConverter.ToUInt32(byteArray, 8);
            iNumberOfPictureinFile = BitConverter.ToUInt32(byteArray, 12);
            iUnknown4 = BitConverter.ToUInt32(byteArray, 16);
            iDataType = BitConverter.ToUInt32(byteArray, 20);
            for (int i = 0; i < iUnknown5.Length; i++)
            {
                iUnknown5[i] = BitConverter.ToUInt32(byteArray, 24 + i * 4);
            }
            iDataSize = BitConverter.ToUInt32(byteArray, 80);
            iUnknown6 = BitConverter.ToUInt32(byteArray, 84);

            size[0] = iUnknown5[6];
            size[1] = iUnknown5[7];
        }

        #endregion

        #region GetterSetter

        public uint IUnknown1 { get => iUnknown1; }
        public uint IUnknown2 { get => iUnknown2; }
        public uint IUnknown3 { get => iUnknown3; }
        public uint INumberOfPictureinFile { get => iNumberOfPictureinFile; set => iNumberOfPictureinFile = value; }
        public uint IUnknown4 { get => iUnknown4; }
        public uint IDataType { get => iDataType; }
        public uint[] IUnknown5 { get => iUnknown5; }
        public uint IDataSize { get => iDataSize; set => iDataSize = value; }
        public uint IUnknown6 { get => iUnknown6; }
        public uint[] Size { get => size; }
        public string Name { get => name; set => name = value; }

        #endregion

        #region Methods

        internal byte[] GetBytes()
        {
            List<byte> byteArray = new List<byte>();

            byteArray.AddRange(BitConverter.GetBytes(iUnknown1));
            byteArray.AddRange(BitConverter.GetBytes(iUnknown2));
            byteArray.AddRange(BitConverter.GetBytes(iUnknown3));
            byteArray.AddRange(BitConverter.GetBytes(iNumberOfPictureinFile));
            byteArray.AddRange(BitConverter.GetBytes(iUnknown4));
            byteArray.AddRange(BitConverter.GetBytes(iDataType));
            for (int i = 0; i < iUnknown5.Length; i++)
            {
                byteArray.AddRange(BitConverter.GetBytes(iUnknown5[i]));
            }
            byteArray.AddRange(BitConverter.GetBytes(iDataSize));
            byteArray.AddRange(BitConverter.GetBytes(iUnknown6));

            return byteArray.ToArray();
        }

        #endregion
    }
}
