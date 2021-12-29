namespace HPT.Gate.DataAccess.Entity.Entity
{
    public class CardUpdate
    {
        public int EmpId { get; set; }

        public int Type { get; set; }

        public string CardNo { get; set; }

        public int CardId { get; set; }

        public int TotalNum { get; set; }

        public int BlackName { get; set; }

        public int CardType { get; set; }

        public int CardCode { get; set; }

        public string Row1 { get; set; }

        public string Row2 { get; set; }

        public string Row3 { get; set; }

        public int InRight { get; set; }

        public int OutRight { get; set; }

        public int VoiceNo { get; set; }

        public int Photo { get; set; }

        public int VacationId { get; set; }

        public int InTimeGroupNo { get; set; }

        public int OutTimeGroupNo { get; set; }

        public string BeginDate { get; set; }

        public string EndDate { get; set; }
        public byte[] FingerPrintData { get; set; }
    }
}
