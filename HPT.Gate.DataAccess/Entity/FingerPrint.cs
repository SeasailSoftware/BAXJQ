namespace HPT.Gate.DataAccess.Entity
{
    public class FingerPrint
    {
        public int EmpId { get; set; }
        public int PositionId { get; set; }
        public int FingerId => EmpId * 5 + 4;
        public byte[] FingerData { get; set; }
    }
}
