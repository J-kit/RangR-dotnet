
using System;
using RangR.Maths.Subtraction;
namespace RangR.Maths.Subtraction
{

    public static class AutoSubtractor
    {
        static AutoSubtractor()
        {
             Subtractor<Byte>.Default = new  ByteSubtractor();	
             Subtractor<Decimal>.Default = new  DecimalSubtractor();	
             Subtractor<Double>.Default = new  DoubleSubtractor();	
             Subtractor<Int16>.Default = new  Int16Subtractor();	
             Subtractor<Int32>.Default = new  Int32Subtractor();	
             Subtractor<Int64>.Default = new  Int64Subtractor();	
             Subtractor<SByte>.Default = new  SByteSubtractor();	
             Subtractor<Single>.Default = new  SingleSubtractor();	
             Subtractor<UInt16>.Default = new  UInt16Subtractor();	
             Subtractor<UInt32>.Default = new  UInt32Subtractor();	
             Subtractor<UInt64>.Default = new  UInt64Subtractor();	
             Subtractor<TimeSpan>.Default = new  TimeSpanSubtractor();	
            
        }

        internal static void EnsureInitialized()
        {
            // Just trigger the static initialization
        }
    }

    public class  ByteSubtractor : Subtractor<Byte>
    {
        public override Byte Subtract(Byte v1, Byte v2)
        {
            return (Byte)(v1 - v2);
        }
    }
    
    public class  DecimalSubtractor : Subtractor<Decimal>
    {
        public override Decimal Subtract(Decimal v1, Decimal v2)
        {
            return (Decimal)(v1 - v2);
        }
    }
    
    public class  DoubleSubtractor : Subtractor<Double>
    {
        public override Double Subtract(Double v1, Double v2)
        {
            return (Double)(v1 - v2);
        }
    }
    
    public class  Int16Subtractor : Subtractor<Int16>
    {
        public override Int16 Subtract(Int16 v1, Int16 v2)
        {
            return (Int16)(v1 - v2);
        }
    }
    
    public class  Int32Subtractor : Subtractor<Int32>
    {
        public override Int32 Subtract(Int32 v1, Int32 v2)
        {
            return (Int32)(v1 - v2);
        }
    }
    
    public class  Int64Subtractor : Subtractor<Int64>
    {
        public override Int64 Subtract(Int64 v1, Int64 v2)
        {
            return (Int64)(v1 - v2);
        }
    }
    
    public class  SByteSubtractor : Subtractor<SByte>
    {
        public override SByte Subtract(SByte v1, SByte v2)
        {
            return (SByte)(v1 - v2);
        }
    }
    
    public class  SingleSubtractor : Subtractor<Single>
    {
        public override Single Subtract(Single v1, Single v2)
        {
            return (Single)(v1 - v2);
        }
    }
    
    public class  UInt16Subtractor : Subtractor<UInt16>
    {
        public override UInt16 Subtract(UInt16 v1, UInt16 v2)
        {
            return (UInt16)(v1 - v2);
        }
    }
    
    public class  UInt32Subtractor : Subtractor<UInt32>
    {
        public override UInt32 Subtract(UInt32 v1, UInt32 v2)
        {
            return (UInt32)(v1 - v2);
        }
    }
    
    public class  UInt64Subtractor : Subtractor<UInt64>
    {
        public override UInt64 Subtract(UInt64 v1, UInt64 v2)
        {
            return (UInt64)(v1 - v2);
        }
    }
    
    public class  TimeSpanSubtractor : Subtractor<TimeSpan>
    {
        public override TimeSpan Subtract(TimeSpan v1, TimeSpan v2)
        {
            return (TimeSpan)(v1 - v2);
        }
    }
    
}