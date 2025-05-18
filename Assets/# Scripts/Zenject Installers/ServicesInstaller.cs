using FriendNote.Configuration;
using FriendNote.Data;
using FriendNote.Data.Repositories;
using FriendNote.Domain;
using Zenject;

public class ServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IDataService>().To<DataService>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<ConfigurationService>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<IRepositoryFactory>().To<RepoFactory>().AsSingle().NonLazy();
        //Container.Bind<SettingService>().FromComponentInHierarchy().AsSingle().NonLazy();
        //Container.Bind<UIService>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}