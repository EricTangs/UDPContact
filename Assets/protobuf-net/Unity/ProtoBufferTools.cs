using UnityEngine;
using System.Collections;

using System.IO;

using System;


namespace ProtoBuf
{

    public class ProtoBufferTools
    {

        public static byte[] Serialize(IExtensible msg)
        {
            byte[] result;
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, msg);
                result = stream.ToArray();
            }
            return result;
        }

        //获取的只是ｐｒｏｔｏｂｕｆｆｅｒ　的ｂｙｔｅ
        public static IExtensible Deserialize<IExtensible>(byte[] message)
        {
            IExtensible result;
            using (var stream = new MemoryStream(message))
            {
                result = Serializer.Deserialize<IExtensible>(stream);
            }
            return result;
        }

        // 获取的是整个消息长度
        public static T  DeserializeBytes<T> (byte[] message)  where T :IExtensible
        {
            int bufferLen = BitConverter.ToInt32(message, 0);
            byte[] tmpByte = new byte[bufferLen];

            Buffer.BlockCopy(message, 7, tmpByte, 0, bufferLen);

            return Deserialize<T>(tmpByte);
        }

    }
}


