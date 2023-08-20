using UnityEngine;
using Zenject;
using Core;
using Utility;
using UI;
using Signals;
using Gameplay;
using Audio;
using Network;
using General;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        //binding
        Container.Bind<StateManager>().AsSingle();
        Container.Bind<GameSettings>().AsSingle();

        Container.BindMediatorView<MainMenuMediator,MainMenuView>();
        Container.BindMediatorView<LoadingUIMediator,LoadingUIView>();
        Container.BindMediatorView<QuizMediator, QuizView>();
        Container.BindMediatorView<MusicMediator, MusicView>();
        Container.BindMediatorView<SoundEffectMediator, SoundEffectView>();
        Container.BindMediatorView<NetworkHandlerMediator,NetworkHandlerView>();
        Container.BindMediatorView<TurnTypeMediator,TurnTypeView>();
        Container.BindMediatorView<ErrorUIMediator, ErrorUIView>();


        //signals
        Container.DeclareSignal<StateChangeSignal>();
        Container.DeclareSignal<StateChangedSignal>();
        Container.DeclareSignal<SetQuestionNumberSignal>();
        Container.DeclareSignal<SetQuestionTimeSignal>();
        Container.DeclareSignal<SetTopScoreSignal>();
        Container.DeclareSignal<AudioBlipSignal>();


    }
}