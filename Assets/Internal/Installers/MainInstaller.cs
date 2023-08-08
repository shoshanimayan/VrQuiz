using UnityEngine;
using Zenject;
using Core;
using Utility;
using UI;
using Signals;
public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        //binding
        Container.Bind<StateManager>().AsSingle();
        Container.BindMediatorView<MainMenuMediator,MainMenuView>();

        //signals
        Container.DeclareSignal<StateChangeSignal>();
        Container.DeclareSignal<StateChangedSignal>();
        Container.DeclareSignal<SetQuestionNumberSignal>();
        Container.DeclareSignal<SetQuestionTimeSignal>();
        Container.DeclareSignal<SetTopScoreSignal>();



    }
}