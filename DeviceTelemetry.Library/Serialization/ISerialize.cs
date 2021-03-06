﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceTelemetry.Library.Serialization
{
    /// <summary>
    /// Interface to serialize & deserialize through the ITransport interface
    /// </summary>
    public interface ISerialize
    {
        byte[] SerializeObject(object Object);
        T DeserializeObject<T>(byte[] bytes) where T : class;
    }
}
