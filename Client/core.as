System.security.allowDomain("*");
if(Ctlapp == undefined) {
	Ctlapp = new Object();
	Ctlapp.Build = new Object();

	Ctlapp.Build.client = function() {
		#include "src\connector.as"
	}

	Ctlapp.Build.app = function() {
		if(this.newClient == undefined) {
			this.newClient = true;
			this.client();
		}
	}

	Ctlapp.Build.app();
}