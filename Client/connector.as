Ctlapp.Connector = new Object();
Ctlapp.Connector.connected = false;

// Connexion au serveur.
Ctlapp.Connector.Socket = new XMLSocket();
Ctlapp.Connector.Socket.onConnect = function (bSuccess) {
    if(bSuccess) {
    	trace("Connection etablished.");
    	this.send("LA");
    } else {
    	trace("Error to connect.");
    }
};
Ctlapp.Connector.Socket.onData = function (msg) {
    trace("RECV: " + msg);
    Ctlapp.Connector.Parser(msg);
};
Ctlapp.Connector.Socket.onClose = function (msg) {
    trace("Connetion closed.");
};

// # Fonctions.
Ctlapp.Connector.log = function(bLogon) {
	if(bLogon) {
		this.Socket.connect("127.0.0.1", "7852");
		trace("Connection to the server...");
	} else {
		this.Socket.close();
	}
}
Ctlapp.Connector.send = function (sData) {
	if(this.connected) {
		this.Socket.send(sData);
		trace("SEND: " + sData);
	}
}

Ctlapp.Connector.Parser = function (sData) {
	switch(sData.charAt(0)) {
		case "L":
			if(sData.charAt(1) == 1) {
    				this.connected = true;
					trace("Authorized for using Ctlapp.");
				} else {
					trace("Connetion error.");
				}
			break;
	}
}
trace("			[Ctlapp DEBUG] Special thanks to \n 					Crystal and Root.\n======================================\n")
Ctlapp.Connector.log(true);

/*
	List of functions :

	FMTitre|Message|[Type]
		[Type] :
			- 0 : Information.
			- 1 : Critical.
			- 2 : Question.
			- 3 : Exclamation.

*/