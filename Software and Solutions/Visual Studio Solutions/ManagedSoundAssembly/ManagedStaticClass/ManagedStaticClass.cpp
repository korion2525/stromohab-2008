// This is the main DLL file.

#include "stdafx.h"

#include "ManagedStaticClass.h"

#include <al.h>
#include <alc.h>
#include <conio.h>
#include <iostream>
#include <time.h>
#include <stdlib.h>
#include <math.h>
#include <string>
#include <fstream>
#include <iostream>

using namespace std;



namespace ALManagedStaticClass
{
		unsigned int buffercount;
		unsigned int buffers[2];
		unsigned int source;
		unsigned int wavSource;
		unsigned int tonefrequency;
		float offset = 0;
		float pitch;
		float gain;
		char *pAudioData;
		const ALCchar *devices;
		const ALCchar *defaultDeviceName;
		ALfloat ListenerOri[6];	// Orientation of the listener. (first 3 elements are "at", second 3 are "up")
		ALCcontext *pContext;
		ALCdevice *pDevice;
		unsigned int wavBuffer;
		//unsigned int numberofbytes;
		//char *pPCMData;
		//ifstream wavFile;

		

	bool ALsetup::SLsetenvironment()//CSLopenalcontext::SLsetenvironment()//
	{
		pAudioData = new char[17640];
		buffercount = 0;
		pitch = 1.0f;
		pDevice = NULL;
		pDevice = alcOpenDevice(NULL);
		if (pDevice != NULL)
		{
			pContext = alcCreateContext(pDevice, NULL);
			if (pContext != NULL)
			{
				alcMakeContextCurrent(pContext);
				alcProcessContext(pContext);
			}
		}
		alGenBuffers(2, buffers);
		alGenBuffers(1, &wavBuffer);
		if (!check())
		{
			return false;
		}
		alListener3f(AL_POSITION, 0.0f, 0.0f, 0.0f);
		alListener3f(AL_VELOCITY, 0.0f, 0.0f, 0.0f);
		ListenerOri[0] = 0.0f; ListenerOri[1] = 0.0f; ListenerOri[2] = -1.0f; ListenerOri[3] = 0.0f; ListenerOri[4] = 1.0f; ListenerOri[5] = 0.0f;
		alListenerfv(AL_ORIENTATION, ListenerOri);

		alGenSources(1, &source);
		alSourcef (source, AL_PITCH, 1.0f);
		alSourcef (source, AL_GAIN, 1.0f);
		alSource3f (source, AL_POSITION, 0.0f, 0.0f, 0.0f);
		alSource3f (source, AL_VELOCITY, 0.0f, 0.0f, 0.0f);

		alGenSources(1, &wavSource);
		alSourcef (wavSource, AL_PITCH,    1.0f     );
		alSourcef (wavSource, AL_GAIN,     1.0f     );
		alSource3f(wavSource, AL_POSITION, 0.0f, 0.0f, 0.0f);
		alSource3f(wavSource, AL_VELOCITY, 0.0f, 0.0f, 0.0f);

		return true;
	}


	bool ALsetup::SLreadWAV(unsigned int bytesize)
	{
		unsigned int numberofbytes = bytesize;
		char *pPCMData;
		ifstream wavFile;
		pPCMData = new char[numberofbytes];
		wavFile.open("c:/Sound/pcm.dat", ios::in|ios::binary);
		if (wavFile.is_open())
		{
			wavFile.read(pPCMData, numberofbytes);
			if (wavFile.eof()) return false;
		}
		else return false;
		
		alBufferData(wavBuffer, AL_FORMAT_STEREO16, pPCMData, numberofbytes, 44100);
		if (!check()) return false;
		alSourcei (wavSource, AL_BUFFER,   wavBuffer);
		return check();
	}


	void ALsetup::SLPlayWAV()
	{
		alSourcePlay(wavSource);
	}

	void ALsetup::SLplayTone()
	{
		tonefrequency = 441;
		pitch = 1.0f;
		SLtonestream(buffers[0]);
		SLtonestream(buffers[1]);
		alSourceQueueBuffers(source, 2, buffers);
		alSourcePlay(source);
	}

	void ALsetup::SLstoptone()
	{
		alSourceStop(source);
		offset = 0;
		int queued;
		alGetSourcei(source, AL_BUFFERS_QUEUED, &queued);
		while(queued--)
		{
			ALuint buffer;
			alSourceUnqueueBuffers(source, 1, &buffer);
			check();
		}
	}

	void ALsetup::SLcloseEnvironment()
	{
		alSourceStop(source);
		int queued;
		alGetSourcei(source, AL_BUFFERS_QUEUED, &queued);
		while(queued--)
		{
			ALuint buffer;
			alSourceUnqueueBuffers(source, 1, &buffer);
			check();
		}
		alDeleteSources(1, &source);
		check();
		alDeleteBuffers(2, buffers);
		check();
		alcMakeContextCurrent(NULL);
		alcDestroyContext(pContext);
		pContext = NULL;
		alcCloseDevice(pDevice);
		pDevice = NULL;
	}

	void ALsetup::SLplayTone(unsigned int freq)
	{
		tonefrequency = freq;
		SLtonestream(buffers[0]);
		SLtonestream(buffers[1]);
		alSourceQueueBuffers(source, 2, buffers);
		alSourcePlay(source);
	}

	bool ALsetup::SLtoneupdate()
	{
		int processed;
		bool active = true;

		alGetSourcei(source, AL_BUFFERS_PROCESSED, &processed);

		while(processed--)//ie. if 1 buffer has been processed
		{
			ALuint buffer;
			alSourceUnqueueBuffers(source, 1, &buffer);//unqueue processed buffer
			active = SLtonestream(buffer);
			if (active == true)
				alSourceQueueBuffers(source, 1, &buffer);
		}
		return active;
	}

	bool ALsetup::SLtoneupdate(unsigned int freq)
	{
		tonefrequency = freq;
		int processed;
		bool active = true;

		alGetSourcei(source, AL_BUFFERS_PROCESSED, &processed);

		while(processed--)//ie. if 1 buffer has been processed
		{
			ALuint buffer;
			alSourceUnqueueBuffers(source, 1, &buffer);//unqueue processed buffer
			active = SLtonestream(buffer);
			if (active == true)
				alSourceQueueBuffers(source, 1, &buffer);
		}
		return active;
	}

	/*bool ALsetup::SLtonestream(ALuint buffer)
	{
		short pcmData[4000];
		void *pcm;
		for (int t = 0;t<4000;t++)
		{
			pcmData[t] = (short)(20000*sin(2*3.141592654*441*t/44100));
		}
		pcm = pcmData;
		alBufferData(buffer, AL_FORMAT_MONO16, pcm, 8000, 44100);
		return true;
	}*/

	bool ALsetup::SLtonestream(ALuint buffer)
	{
		short pcmData[8000];
		void *pcm;
		float angleinc = (float)(360*tonefrequency/44100);
		int t = 0;
		float theta = offset;
		while (t<8000)
		{
			while ((t<8000)&&(theta<360))
			{
				pcmData[t] = (short)((20000*sin(theta*3.141592654/180))+(12000*sin(theta*3.141592654/90))+(6000*sin(theta*3.141592654/60))+(3000*sin(theta*3.141592654/45)));
				t++;
				theta += angleinc;
			}
			if (theta >= 360) theta -= 360;
		}
		offset = theta;
		pcm = pcmData;
		alBufferData(buffer, AL_FORMAT_MONO16, pcm, 16000, 44100);
		return true;
	}

	void ALsetup::SLvarygain(float g)
	{
		gain = g;
		alSourcef(wavSource, AL_GAIN, gain);
	}


	void ALsetup::SLvarypitch(float p)
	{
		pitch += p;
		alSourcef(source, AL_PITCH, pitch);
	}

	void ALsetup::SLchangepitch(float pitch)
	{
		alSourcef(source, AL_PITCH, pitch);
	}

	bool ALsetup::check()
	{
		if (alGetError()==AL_NO_ERROR)
			return true;
		else return false;
	}

}