#include "Frame.h"

Frame* Frame::createFrame(FrameType type) {
	switch(type) {
		case Single: return new SingleFrame();
		case Double: return new DoubleFrame();
		case Thick: return new ThickFrame();
		case Block: return new BlockFrame();
	}
	return new NoFrame();
}
