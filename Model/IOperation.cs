namespace Model
{
    internal interface IOperation
    {
        void Do();
        void Undo();
    }
}
