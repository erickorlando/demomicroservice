namespace Entity
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public bool RowStatus { get; set; }

        protected EntityBase()
        {
            RowStatus = true;
        }
    }
}