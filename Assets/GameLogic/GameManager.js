#pragma strict


function Start () {
	retrieveRandomCodeblock();
	// get codeBlock from DB
	splitCodeblockIntoLetters();
	createGalleryOfLetters();
	startRound();
}

public function startRound() {
	// letters lerp from ground
	// play starting sound
	//

}

private function createGalleryOfLetters() {
	// instantiate a prefab for each letter
	// set transforms for each letter at gallery starting points
}


private function retrieveRandomCodeblock() {
	// search library of code to retrieve a component
}

var codeBlock : String = '<App component />';
var codeLine : Array = new Array[codeBlock.length];
private function splitCodeblockIntoLetters() {
	Debug.Log(codeBlock.length);
	Debug.Log(codeLine.length);
	// codeLine = codeBlock.Split(separator);
	for (var i = 0; i < codeBlock.length; i++) {
		codeLine[i] = codeBlock[i].ToString();
	}
	Debug.Log(codeLine[0]);
	// string manipulation on codeblock to split into array of chars
	// watch for spaces
}

var currentTargetCharCode = 98;
function Update () {
	var asciiCodeOfKeyPressed : int = checkKeyboardInput();
	if (asciiCodeOfKeyPressed == currentTargetCharCode) {
		Debug.Log('shot hit!');
	}


}

public function checkKeyboardInput () {
	if (Input.anyKeyDown) {
		for (var vKey : KeyCode in System.Enum.GetValues(typeof(KeyCode))) {
			var asciiCode : int = parseInt(vKey);
			if (Input.GetKeyDown(vKey) && asciiCode >= 32 && asciiCode <= 126) {
				return asciiCode;
				// TODO: Handle shift key
			}
		}
		return;
	}

	// listen for key presses
		// check for current letter (snap to current)
		// if input letter matches current letter
			// destroyLetter()
			// snapCameraToNextLetter()
			// incrementPoints()
			// increment points
		// else
			// destroy streak
			// return
}
