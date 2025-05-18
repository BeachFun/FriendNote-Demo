using FriendNote.UI;
using Zenject;

public class ManagersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IPagesController>().To<PagesController>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
