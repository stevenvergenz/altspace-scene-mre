import { resolve } from 'path';
import * as MRE from '@microsoft/mixed-reality-extension-sdk';

const server = new MRE.WebHost({
	baseDir: resolve(__dirname, '../public')
});

server.adapter.onConnection(context => new App(context));

class App {
	private door: MRE.Actor;
	private box: MRE.Actor;
	private button: MRE.Actor;
	private activationTimeout: NodeJS.Timeout;

	constructor(private context: MRE.Context) {
		this.context.onStarted(() => console.log('started'));
		this.context.onStopped(() => console.log('stopped'));
		this.context.onActorCreated(actor => {
			let updateInteraction = false;
			switch (actor.name) {
				case '01_low':
					this.door = actor;
					updateInteraction = true;
					break;
				case 'GrabCube':
					this.box = actor;
					updateInteraction = true;
					break;
				case 'Button':
					this.button = actor;
					updateInteraction = true;
					break;
				case 'AnimCube':
					setTimeout(() => {
						console.log(actor.animationsByName);
						actor.animationsByName.get('Spin').play();
					}, 100);
					break;
				
			}

			if (updateInteraction && this.door && this.box && this.button) {
				this.hookTriggers();
			}
		});
	}

	private hookTriggers() {
		this.box.enableRigidBody({ mass: 5 });
		this.box.grabbable = true;
		this.box.onGrab('begin', () => this.box.rigidBody.isKinematic = true);
		this.box.onGrab('end', () => this.box.rigidBody.isKinematic = false);

		this.button.enableRigidBody({
			isKinematic: true,
			detectCollisions: true
		});
		this.button.collider.onCollision('collision-enter', data => {
			if (data.otherActor === this.box) {
				this.activationTimeout = setTimeout(() => this.activateButton(), 1000);
			}
		});
		this.button.collider.onCollision('collision-exit', data => {
			if (data.otherActor === this.box && this.activationTimeout) {
				clearTimeout(this.activationTimeout);
				this.activationTimeout = null;
			}
		});
	}

	private activateButton() {
		console.log('switch activated');
		this.button.animateTo(
			{ transform: { local: { position: this.button.transform.local.position.subtractFromFloats(0, 0.133, 0) } } },
			0.5,
			MRE.AnimationEaseCurves.EaseOutQuadratic
		);
		this.door.animateTo(
			{ transform: { local: { rotation: MRE.Quaternion.FromEulerAngles(-Math.PI / 2, 0, -Math.PI / 2) } } },
			1,
			MRE.AnimationEaseCurves.EaseInOutQuadratic
		);
	}
}
