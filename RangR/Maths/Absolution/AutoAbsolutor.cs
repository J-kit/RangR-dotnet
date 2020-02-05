
using System;
using RangR.Maths.Absolution;

    public static partial class Absolutor
    {
        static Absolutor()
        {
             Absolutor<Byte>.Default = new  ByteAbsolutor();	
             Absolutor<Decimal>.Default = new  DecimalAbsolutor();	
             Absolutor<Double>.Default = new  DoubleAbsolutor();	
             Absolutor<Int16>.Default = new  Int16Absolutor();	
             Absolutor<Int32>.Default = new  Int32Absolutor();	
             Absolutor<Int64>.Default = new  Int64Absolutor();	
             Absolutor<SByte>.Default = new  SByteAbsolutor();	
             Absolutor<Single>.Default = new  SingleAbsolutor();	
             Absolutor<UInt16>.Default = new  UInt16Absolutor();	
             Absolutor<UInt32>.Default = new  UInt32Absolutor();	
             Absolutor<UInt64>.Default = new  UInt64Absolutor();	
            
        }

        internal static void EnsureInitialized()
        {
            
        }
    }

    public class  ByteAbsolutor : Absolutor<Byte>
    {
        public override Byte Abs(Byte v1)
        {
             return (Byte)Math.Abs(v1); 
        }
    }
    
    public class  DecimalAbsolutor : Absolutor<Decimal>
    {
        public override Decimal Abs(Decimal v1)
        {
             return (Decimal)Math.Abs(v1); 
        }
    }
    
    public class  DoubleAbsolutor : Absolutor<Double>
    {
        public override Double Abs(Double v1)
        {
             return (Double)Math.Abs(v1); 
        }
    }
    
    public class  Int16Absolutor : Absolutor<Int16>
    {
        public override Int16 Abs(Int16 v1)
        {
             return (Int16)Math.Abs(v1); 
        }
    }
    
    public class  Int32Absolutor : Absolutor<Int32>
    {
        public override Int32 Abs(Int32 v1)
        {
             return (Int32)Math.Abs(v1); 
        }
    }
    
    public class  Int64Absolutor : Absolutor<Int64>
    {
        public override Int64 Abs(Int64 v1)
        {
             return (Int64)Math.Abs(v1); 
        }
    }
    
    public class  SByteAbsolutor : Absolutor<SByte>
    {
        public override SByte Abs(SByte v1)
        {
             return (SByte)Math.Abs(v1); 
        }
    }
    
    public class  SingleAbsolutor : Absolutor<Single>
    {
        public override Single Abs(Single v1)
        {
             return (Single)Math.Abs(v1); 
        }
    }
    
    public class  UInt16Absolutor : Absolutor<UInt16>
    {
        public override UInt16 Abs(UInt16 v1)
        {
             return v1; 
        }
    }
    
    public class  UInt32Absolutor : Absolutor<UInt32>
    {
        public override UInt32 Abs(UInt32 v1)
        {
             return v1; 
        }
    }
    
    public class  UInt64Absolutor : Absolutor<UInt64>
    {
        public override UInt64 Abs(UInt64 v1)
        {
             return v1; 
        }
    }
    