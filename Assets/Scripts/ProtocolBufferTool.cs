using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using test;
using ProtoBuf;
using System.IO;
using System;
using System.Text;


public class ProtocolBufferTool{
    /// <summary>
    /// 将protobuff.cs变成bytes
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static byte[] Serialize(IExtensible msg)
    {
        byte[] result;
        using (var stream=new MemoryStream())
        {
            Serializer.Serialize(stream, msg);
            result = stream.ToArray();
        }
        return result;
    }

    //将byte数组变成protobuff的类
    public static IExtensible Deserialize<IExtensible>(byte[] message)
    {
        IExtensible result;
        using (var stream=new MemoryStream(message))
        {
            result = Serializer.Deserialize<IExtensible>(stream);
        }
        return result;
    }

}
