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
			console.log(actor.name);
			switch (actor.name) {
				case '01_low':
					this.door = actor;
					break;
				case 'Box':
					this.box = actor;
					break;
				case 'Button':
					this.button = actor;
					break;
			}

			if (this.door && this.box && this.button) {
				this.hookTriggers();
			}
		});
	}

	private hookTriggers() {
		this.box.enableRigidBody({ mass: 5 });
		this.box.grabbable = true;

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

	}
}
