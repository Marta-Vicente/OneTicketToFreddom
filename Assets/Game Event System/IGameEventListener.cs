public interface IGameEventListener<in TParameter>
{
    void OnEventRaised(TParameter t);
}
