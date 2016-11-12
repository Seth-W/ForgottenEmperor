namespace CFE
{
    interface IAction
    {
        bool execute();
        bool revert();
        bool cancel();
    }
}
