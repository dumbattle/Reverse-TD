namespace Core {
    public interface IFSM_State {
        IFSM_State Update();
    }
    public interface IFSM_State<T> {
        IFSM_State<T> Update(T t);
    }
}
