// This is the main DLL file.

#include "stdafx.h"

#include "ManagedStaticClass.h"




namespace OpenALManager
{
		unsigned int buffercount;
		unsigned int buffers[24];
		unsigned int wavBuffers[2];
		unsigned int sources[12];
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
		//unsigned int watch = 1;
		float angleinc = 1;
		char* pathname;
		bool wavPlay = true;



	

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
		alGenBuffers(12, buffers);
		alGenBuffers(2, wavBuffers);
		if (check()!=6)
		{
			return false;
		}
		alListener3f(AL_POSITION, 0.0f, 0.0f, 0.0f);
		alListener3f(AL_VELOCITY, 0.0f, 0.0f, 0.0f);
		ListenerOri[0] = 0.0f; ListenerOri[1] = 0.0f; ListenerOri[2] = -1.0f; ListenerOri[3] = 0.0f; ListenerOri[4] = 1.0f; ListenerOri[5] = 0.0f;
		alListenerfv(AL_ORIENTATION, ListenerOri);

		//alGenSources(1, &source);
		//alSourcef (source, AL_PITCH, 1.0f);
		//alSourcef (source, AL_GAIN, 1.0f);
		//alSource3f (source, AL_POSITION, 0.0f, 0.0f, 0.0f);
		//alSource3f (source, AL_VELOCITY, 0.0f, 0.0f, 0.0f);

		alGenSources(1, &wavSource);
		alSourcef (wavSource, AL_PITCH,    1.0f     );
		alSourcef (wavSource, AL_GAIN,     1.0f     );
		alSource3f(wavSource, AL_POSITION, 0.0f, 0.0f, 0.0f);
		alSource3f(wavSource, AL_VELOCITY, 0.0f, 0.0f, 0.0f);

		return true;
	}

	bool ALsetup::GenerateSources()
	{
		bool err = 0;
		for (int i = 0; i < 12; i++)
		{
			alGenSources(1, &sources[i]);
			alSourcef (sources[i], AL_PITCH, 1.0f);
			alSourcef (sources[i], AL_GAIN, 1.0f);
			alSource3f (sources[i], AL_POSITION, 0.0f, 0.0f, 0.0f);
			alSource3f (sources[i], AL_VELOCITY, 0.0f, 0.0f, 0.0f);
			if (check() != 6) err = 1;
		}
		return err;
	}

	int ALsetup::GenerateSource(int sourceNumber)
	{
		alGenSources(1, &sources[sourceNumber]);
		alSourcef (sources[sourceNumber], AL_PITCH, 1.0f);
		alSourcef (sources[sourceNumber], AL_GAIN, 1.0f);
		alSource3f (sources[sourceNumber], AL_POSITION, 0.0f, 0.0f, 0.0f);
		alSource3f (sources[sourceNumber], AL_VELOCITY, 0.0f, 0.0f, 0.0f);
		return check();
	}

	void ALsetup::SLbufferData_unsafe(short pcmData[], int count)
	{
		alBufferData(buffers[0], AL_FORMAT_MONO16, (void*)pcmData, count, 44100);
		alSourcei(source, AL_BUFFER, buffers[0]);
		alSourcePlay(source);
		
	}
	
	void ALsetup::BufferMonoData(int sourceID, int pcmData, int count)
	{
		alBufferData(buffers[sourceID * 2], AL_FORMAT_MONO16, (void*)pcmData, count*2, 44100);
		check();
		alSourcei(sources[sourceID], AL_BUFFER, buffers[sourceID*2]);
		check();
	}

	void ALsetup::BufferStereoData1(int pcmData, int count)
	{
		alBufferData(buffers[0], AL_FORMAT_STEREO16, (void*)pcmData, count*2, 44100);
		
		alSourceQueueBuffers(source, 1, &buffers[0]);
	
	}

	void ALsetup::BufferStereoData2(int pcmData, int count)
	{
		alBufferData(buffers[1], AL_FORMAT_STEREO16, (void*)pcmData, count*2, 44100);
		
		alSourceQueueBuffers(source, 1, &buffers[1]);
		
	}

	/*void ALsetup::BufferStereoData(int pcmData, int count)
	{
		alBufferData(buffers[0], AL_FORMAT_STEREO16, (void*)pcmData, count, 44100);
	}*/

	void ALsetup::BufferSource(int sourceID)
	{
		alSourcei(sources[sourceID], AL_BUFFER, buffers[sourceID*2]);
		check();
	}

	void ALsetup::PlaySource(int source)
	{
		alSourcePlay(sources[source]);
		check();
	}

	void ALsetup::StopSource(int sourceID)
	{
		alSourceStop(sources[sourceID]);
		check();
	}

	int ALsetup::UnBufferSource(int sourceID)
	{
		alSourcei(sources[sourceID], AL_BUFFER, NULL);
		return check();
	}

	int ALsetup::BuffersQueued(int sourceID)
	{
		int queued;
		alGetSourcei(sources[sourceID], AL_BUFFERS_QUEUED, &queued);
		return queued;
	}

	//int ALsetup::CleanSource()
	//{
	//	wavPlay = false;
	//	angleinc = 0;
	//	alSourceStop(source);
	//	RemoveAllBuffers();
	//	UnBufferSource();
	//	return check();
	//}

	int ALsetup::RemoveAllBuffers(int sourceID)
	{
		int queued;
		alGetSourcei(sources[sourceID], AL_BUFFERS_QUEUED, &queued);
		while(queued--)
		{
			ALuint buffer;
			alSourceUnqueueBuffers(sources[sourceID], 1, &buffer);
		}
		return check();
	}

	int ALsetup::SourceState()
	{
		ALenum state;
		alGetSourcei(source, AL_SOURCE_STATE, &state);
		switch(state)
		{
		case AL_PLAYING:
			return 3;
			break;
		case AL_PAUSED:
			return 2;
			break;
		case AL_INITIAL:
			return 1;
			break;
		case AL_STOPPED:
			return 0;
			break;
		default:
			return 9;
		}
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

	//void ALsetup::Play(int ptr)
	//{
	//	wavPlay = true;
	//	char* pC = (char*)ptr;
	//	ALwav music;
	//	music.SLplayWAV(pC, wavBuffers, source);
	//	while(music.SLupdate() && wavPlay);
	//}


	
	

	void ALsetup::SLcloseEnvironment(int sourceCount)
	{
		for (int i = 0;i<sourceCount;i++)
		{
			alSourceStop(sources[i]);
			int queued;
			alGetSourcei(sources[i], AL_BUFFERS_QUEUED, &queued);
			while(queued--)
			{
				ALuint buffer;
				alSourceUnqueueBuffers(sources[i], 1, &buffer);
				check();
			}
			alDeleteSources(1, &sources[i]);
			check();
		}
		alDeleteBuffers(12, buffers);
		alDeleteBuffers(2, wavBuffers);
		check();
		alcMakeContextCurrent(NULL);
		alcDestroyContext(pContext);
		pContext = NULL;
		alcCloseDevice(pDevice);
		pDevice = NULL;
	}

	void ALsetup::SLplayTone(unsigned int freq)
	{
		//cout << "Starting tone";
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

	

	float ALsetup::SLtoneupdate(unsigned int freq)
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
		return angleinc;
	}


	bool ALsetup::SLtonestream(ALuint buffer)
	{
		short pcmData[4000];
		//void *pcm;
		//float angleinc = (float)(360*tonefrequency/44100);
		angleinc = (float)(360.0f*(float)tonefrequency/44100.0f);
		int t = 0;
		float theta = offset;
		while (t<4000)
		{
			while ((t<4000)&&(theta<360))
			{
				pcmData[t] = (short)((20000*sin(theta*3.141592654/180)));//+(12000*sin(theta*3.141592654/90))+(6000*sin(theta*3.141592654/60))+(3000*sin(theta*3.141592654/45)));
				t++;
				theta += angleinc;
			}
			if (theta >= 360) theta -= 360;
		}
		offset = theta;
		//pcm = pcmData;
		alBufferData(buffer, AL_FORMAT_MONO16, (void*)pcmData, 8000, 44100);
		return true;
	}

	void ALsetup::SLvarygain(float gain)
	{
		//gain = g;
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

	void ALsetup::ChangeSourcePitch(int sourceID, float pitch)
	{
		alSourcef(sources[sourceID], AL_PITCH, pitch);
	}

	int ALsetup::check()
	{
		int err;
		try
		{
			if (alcGetError(pDevice)==ALC_NO_ERROR) err = 0;
			if (alcGetError(pDevice)==ALC_INVALID_DEVICE) err = 1;
			if (alcGetError(pDevice)==ALC_INVALID_CONTEXT) err = 2;
			if (alcGetError(pDevice)==ALC_INVALID_ENUM) err = 3;
			if (alcGetError(pDevice)==ALC_INVALID_VALUE) err = 4;
			if (alcGetError(pDevice)==ALC_OUT_OF_MEMORY) err = 5;
			if (alGetError()==AL_NO_ERROR) err = 6;
			if (alGetError()==AL_INVALID_NAME) err = 7;
			if (alGetError()==AL_INVALID_ENUM) err = 8;
			if (alGetError()==AL_INVALID_VALUE) err = 9;
			if (alGetError()==AL_INVALID_OPERATION) err = 10;
			if (alGetError()==AL_OUT_OF_MEMORY) err = 11;
			if ((err == 0)||(err == 6))
			{
				return err;
			}
			else
			{
				return err;
			}
		}
		catch (exception& e)
		{
			return 13;
		}
		
	};

	int ALsetup::BuffersProcessed(int sourceNumber)
	{
		int processed;
		alGetSourcei(sources[sourceNumber], AL_BUFFERS_PROCESSED, &processed);
		return processed;
	}

	int ALsetup::UnqueueBuffer()
	{
		ALuint buffer;
		alSourceUnqueueBuffers(source, 1, &buffer);
		return check();
	}

	//int ALsetup::QueueWavBuffers()
	//{
	//}

	//int ALsetup::BufferStereoData()
	//{
	//}

	void ALsetup::InitialiseMonoStreamBuffers(int pcmBufferData1, int pcmBufferData2, int count)
	{
		alBufferData(buffers[0], AL_FORMAT_MONO16, (void*)pcmBufferData1, count*2, 44100);
		alBufferData(buffers[1], AL_FORMAT_MONO16, (void*)pcmBufferData2, count*2, 44100);
		alSourceQueueBuffers(source, 2, buffers);
	}

	int ALsetup::InitialiseStereoStreamBuffers(int pcmBufferData1, int pcmBufferData2, int count)
	{
		if (pcmBufferData1 != 0)
		{
			alBufferData(wavBuffers[0], AL_FORMAT_STEREO16, (void*)pcmBufferData1, count*2, 44100);
			alSourceQueueBuffers(source, 1, &wavBuffers[0]);
		}
		if (pcmBufferData2 != 0)
		{
			alBufferData(wavBuffers[1], AL_FORMAT_STEREO16, (void*)pcmBufferData2, count*2, 44100);
			alSourceQueueBuffers(source, 1, &wavBuffers[1]);
		}
		//alSourceQueueBuffers(source, 2, wavBuffers);
		return check();
	}

	void ALsetup::QueueMonoBuffer(int sourceBuffer, int pcmData, int count)
	{
		alBufferData(buffers[sourceBuffer], AL_FORMAT_MONO16, (void*)pcmData, count*2, 44100);
		check();
		alSourceQueueBuffers(sources[sourceBuffer], 1, &buffers[sourceBuffer]);
		check();
	}

	void ALsetup::QueueMonoBuffer1(int sourceID, int pcmData, int count)
	{
		alBufferData(buffers[sourceID], AL_FORMAT_MONO16, (void*)pcmData, count*2, 44100);
		check();
		alSourceQueueBuffers(sources[sourceID], 1, &buffers[sourceID]);
		check();
	}

	void ALsetup::QueueMonoBuffer2(int sourceID, int pcmData, int count)
	{
		alBufferData(buffers[sourceID+1], AL_FORMAT_MONO16, (void*)pcmData, count*2, 44100);
		check();
		alSourceQueueBuffers(sources[sourceID], 1, &buffers[sourceID+1]);
		check();
	}

	int ALsetup::StreamStereoData(int pcmData, int count)
	{
		ALuint buffer;
		alSourceUnqueueBuffers(source, 1, &buffer);
		alBufferData(buffer, AL_FORMAT_STEREO16, (void*)pcmData, count*2, 44100);
		alSourceQueueBuffers(source, 1, &buffer);
		return check();
	}

	int ALsetup::StreamMonoData(int sourceID, int pcmData, int count)
	{
		ALuint buffer;
		alSourceUnqueueBuffers(sources[sourceID], 1, &buffer);
		alBufferData(buffer, AL_FORMAT_MONO16, (void*)pcmData, count*2, 44100);
		alSourceQueueBuffers(sources[sourceID], 1, &buffer);
		return check();
	}


	ALwav::ALwav()
	{
		pAudioData = new char[8820];
	}

	ALwav::~ALwav()
	{
		delete [] pAudioData;
	}

	void ALwav::SLplayWAV(const char* pathname, ALuint buffers[2], ALuint source)
	{
		wavInfo wavHeader;
		fmtInfo fmtHeader;
		fmtData formatting;
		dataInfo dataHeader;
		chunkInfo currentChunk;
		bytecount = 0;
		if (SLParseWAV2(pathname, &wavHeader, &fmtHeader, &formatting, &dataHeader, &currentChunk))
		{
			SLwavstream(buffers[0]);
			SLwavstream(buffers[1]);
			alSourceQueueBuffers(source, 2, buffers);
			alSourcePlay(source);
		}
		else
		{
			//cout << "Unable to load WAVE file.";
		}
	}

	bool ALwav::SLupdate()
	{
		int processed;
		bool active = true;

		alGetSourcei(source, AL_BUFFERS_PROCESSED, &processed);

		while(processed--)//ie. if 1 buffer has been processed
		{
			ALuint buffer;
			alSourceUnqueueBuffers(source, 1, &buffer);//unqueue processed buffer
			active = SLwavstream(buffer);
			if (active == true)
				alSourceQueueBuffers(source, 1, &buffer);
		}
		return active;
	}

	bool ALwav::SLwavstream(ALuint buffer)//reads pcm data from file and loads into (streaming) buffer
	{
		wavFile.read(pAudioData, 8820);
		if (wavFile.eof()) return false;
		alBufferData(buffer, AL_FORMAT_STEREO16, pAudioData, 8820, 44100);
		//bytecount += 8820;
		return true;
	}

	bool ALwav::SLParseWAV2(const char *wavPath, wavInfo *wavHeader, fmtInfo *fmtHeader, fmtData *formatting, dataInfo *dataHeader, chunkInfo *currentChunk)
	{

		wavFile.open(wavPath, ios::in|ios::binary|ios::beg);
		if (wavFile.is_open())
		{
			wavFile.read((char*)wavHeader, sizeof(wavInfo));

			if ((memcmp(wavHeader->strRIFF, "RIFF", 4) == 0 ) && (memcmp(wavHeader->strWAVE, "WAVE", 4) == 0))//this confirms main WAV header is correct
			{
				//cout << "RIFF" << wavHeader->ulRIFFsize << "WAVE\n";
				int d = 0;//debug
				while (!CheckWAVHeader("fmt ", currentChunk))//check for the "format header"
				{
					if (memcmp(currentChunk->strChunk, "LIST", 4) == 0 )
					{
						wavFile.seekg(currentChunk->ulChunkSize, ios::cur);
					}
					else
					{
						wavFile.seekg(fmtHeader->ulChunkSize, ios::cur);//move stream pointer along to the beginning of the next chunk
						//if (d++ == 50) return false;//debug
					}
				}
				memcpy(fmtHeader->strFMT, currentChunk->strChunk, 4);//fmtHeader->strFMT = currentChunk.strChunk;
				fmtHeader->ulChunkSize = currentChunk->ulChunkSize;
				switch (fmtHeader->ulChunkSize)
				{
				case 16:
					wavFile.read((char*)formatting, sizeof(fmtData));
					//cout << formatting->usFormat << "\n" << formatting->usChannels << "\n";
					//cout << formatting->ulSampleRate << "\n" << formatting->ulByteRate << "\n";
					//cout << formatting->usBlock << "\n" << formatting->usBits << "\n";
					break;

				case 18:
					wavFile.read((char*)formatting, sizeof(fmtData));
					//cout << formatting->usFormat << "\n" << formatting->usChannels << "\n";
					//cout << formatting->ulSampleRate << "\n" << formatting->ulByteRate << "\n";
					//cout << formatting->usBlock << "\n" << formatting->usBits << "\n";
					wavFile.seekg(2, ios::cur);
					break;

				case 40:
					wavFile.read((char*)formatting, sizeof(fmtData));
					//cout << formatting->usFormat << "\n" << formatting->usChannels << "\n";
					//cout << formatting->ulSampleRate << "\n" << formatting->ulByteRate << "\n";
					//cout << formatting->usBlock << "\n" << formatting->usBits << "\n";
					wavFile.seekg(24, ios::cur);
					break;

				default:
					//cout << "WAVE file containing uncompressed PCM data required.";
					return false;
				}
				int c = 0;
				while (!CheckWAVHeader("data", currentChunk))
				{
					wavFile.seekg(currentChunk->ulChunkSize, ios::cur);
				}
				memcpy(dataHeader->strData, currentChunk->strChunk, 4);
				dataHeader->ulLength = currentChunk->ulChunkSize;
				//TO DO: need exception handling for bad files/bad reads or to avoid going past end of files in the 'while();' loops

				return true;
			}
		}
		return false;
	}

	bool ALwav::CheckWAVHeader(char *required_name, chunkInfo *chunkHeader)
	{
		wavFile.read((char*)chunkHeader, sizeof(chunkInfo));
		//cout << chunkHeader->strChunk << "\n" << chunkHeader->ulChunkSize << "\n";
		return (memcmp(chunkHeader->strChunk, required_name, 4) == 0);
	};

}