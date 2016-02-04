#include "ofApp.h"

//--------------------------------------------------------------
void ofApp::setup(){

	
	ofSetFrameRate(30); //set Frame Rate to 30
	ofSetVerticalSync(true); //set V-Sync
	kinect.setup();
	//GUI panel for positional debugging
	//gui.setup("panel");

	//load fonts
	player.init();


	/* for debugging
	gui.setup("panel"); // most of the time you don't need a name but don't forget to call setup
	gui.add(distance1.set("PARAM1", 0,0,100));
	gui.add(distance2.set("PARAM2", 0, 0, 100));
	gui.add(distance3.set("PARAM3", 0, 0, 100));
	gui.add(distance4.set("PARAM4", 0, 0, 100));
	*/

	ofAddListener(player.eventEnter, this, &ofApp::activateSelectionScreen);
	ofAddListener(player.launchApp, this, &ofApp::launchSelectedApp);
}


//--------------------------------------------------------------
void ofApp::update(){

}

//--------------------------------------------------------------
void ofApp::draw(){

	ofBackground(ofColor::blueSteel);

	//gui.draw();

	switch (appState)
	{
		//start page
	case 0:
		player.drawTitlePage();
		player.drawInputText();
		/* disabled cursor unless needed
		ofPushMatrix();
		ofScale(15, 15);
		player.drawCursor();
		ofPopMatrix();
		*/
		break;

		//game selection screen
	case 1:
		player.drawAppSelectionPage();
		break;
	case 2:
		drawRunningApp();
		//default is just to load the start page
	default:
		//player.drawTitlePage();
		//player.drawInputText();
		break;
	}

	//cout << "X: " << mouseX << ", Y: " << mouseY << "\n";
}

void ofApp::activateSelectionScreen()
{
	if(appState==0)
		appState = 1; //switch to Game Selection Screen

	player.lastResetTime = ofGetElapsedTimef();
}

void ofApp::launchSelectedApp( int &i)
{
	appState = 2;
	switch (i)
	{
	case 1:
		player.activeApp = 1;
		break;
	case 2:
		player.activeApp = 2;
		break;
	case 3:
		player.activeApp = 3;
		break;
	}
}

void ofApp::drawRunningApp()
{
	ofVec3f temp;
	ofVec3f tempRight;
	//cout << player.activeApp;
	kinect.update();
	kinect.draw();
	if (player.rightSideActivated)
		temp = kinect.rightPos;
	else
		temp = kinect.leftPos;
	tempRight = kinect.rightPos;
	ofVec3f final((abs(temp.x) * 1919) , (abs(temp.y) * 1079) * 1.5, 0);
	ofVec3f finalRight((abs(tempRight.x) * 1919), (abs(tempRight.y) * 1079) * 1.5, 0);
	switch (player.activeApp)
	{
	case 1:
		//cout << "X: " << (abs(temp.x) *1919) << " and Y: " <<(abs(temp.y) * 1079) * 1.5 << "\n";
		player.drawingChallengePage(final.x+300, final.y+100, 0,0);
		//player.drawingChallengePage((float)mouseX, (float)mouseY,0,0);
		break;
	case 2:
		// to be used once kinect coordinates added 
		//player.matrixMatchingPage(final.x + 300, final.y + 300, finalRight.x, finalRight.y);
		player.matrixMatchingPage();
		break;
	case 3:
		//player.musicConductorPage(final.x*2, final.y*1.75);
		player.musicConductorPage((float)mouseX, (float)mouseY);
		break;
	}
}
//for future use
void ofApp::addText(string &s) {
	textEntries.push_back(s);
	player.clear();
}
//--------------------------------------------------------------
//for future use
void ofApp::drawEntries() {
	for (int i = 0; i<textEntries.size(); ++i) {
		ofDrawBitmapString(textEntries[i], 50, 100 + 10 * i);
	}
}
//--------------------------------------------------------------
void ofApp::keyPressed(int key){

	kinect.keyPressed(key);
}

//--------------------------------------------------------------
void ofApp::keyReleased(int key){

}

//--------------------------------------------------------------
void ofApp::mouseMoved(int x, int y ){

}

//--------------------------------------------------------------
void ofApp::mouseDragged(int x, int y, int button){

	if (player.activeApp == 2)
	{
		player.mouseDragged(x, y, button);
	}
}

//--------------------------------------------------------------
void ofApp::mousePressed(int x, int y, int button){

	if (button == 0)
	{
		player.mouseEvent(mouseX,mouseY,appState);
	}
	kinect.mousePressed(x, y, button);

}

//--------------------------------------------------------------
void ofApp::mouseReleased(int x, int y, int button){
	if (player.activeApp == 2)
	{
		player.mouseReleased(x, y, button);
	}
}

//--------------------------------------------------------------
void ofApp::mouseEntered(int x, int y){

}

//--------------------------------------------------------------
void ofApp::mouseExited(int x, int y){

}

//--------------------------------------------------------------
void ofApp::windowResized(int w, int h){

}

//--------------------------------------------------------------
void ofApp::gotMessage(ofMessage msg){

}

//--------------------------------------------------------------
void ofApp::dragEvent(ofDragInfo dragInfo){ 

}
