// ManagedStaticClass.h

#pragma once

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
#include <string.h>
#include <exception>


using namespace System;
using namespace std;


namespace OpenALManager {

	public ref class ALsetup
	{
		

	public:
		static bool SLsetenvironment();
		static int GenerateSource(int sourceNumber);
		static bool GenerateSources();
		static void SLplayTone();
		static void SLchangepitch(float);
		static void ChangeSourcePitch(int sourceID, float pitch);
		static bool SLtonestream(unsigned int buffer);
		static bool SLtoneupdate();
		static float SLtoneupdate(unsigned int freq);
		static void SLplayTone(unsigned int freq);
		static void SLvarypitch(float p);
		static void SLstoptone();
		static void SLcloseEnvironment(int sourceCount);
		static void SLvarygain(float g);
		//static void Play(int);
		static void SLbufferData_unsafe(short[], int);
		static void BufferMonoData(int sourceID, int pcmData, int count);
		static void BufferSource(int sourceID);
		static void PlaySource(int source);
		//static void StopSource();
		static void StopSource(int sourceID);
		static int UnBufferSource(int sourceID);
		static int BuffersQueued(int sourceID);
		static int RemoveAllBuffers(int sourceID);
		static int SourceState();
		//static int CleanSource();
		static int BuffersProcessed(int sourceNumber);
		static int UnqueueBuffer();
		//static int QueueWavBuffers();
		//static int BufferStereoData();
		static int InitialiseStereoStreamBuffers(int pcmBufferData1, int pcmBufferData2, int count);
		static int StreamStereoData(int pcmData, int count);
		static void BufferStereoData1(int pcmData, int count);
		static void BufferStereoData2(int pcmData, int count);
		static void InitialiseMonoStreamBuffers(int pcmBufferData1, int pcmBufferData2, int count);
		static int StreamMonoData(int sourceID, int pcmData, int count);
		static void QueueMonoBuffer1(int sourceID, int pcmData, int count);
		static void QueueMonoBuffer2(int sourceID, int pcmData, int count);
		static void QueueMonoBuffer(int sourceBuffer, int pcmData, int count);
		static int check();
	};

	public class ALwav
	{
		char *pAudioData;
		const char *pathname;
		ifstream wavFile;
		int bytecount;
		

		struct wavInfo
		{
			char strRIFF[4];
			unsigned long ulRIFFsize;
			char strWAVE[4];
		};

		struct fmtInfo
		{
			unsigned char strFMT[4];
			unsigned long ulChunkSize;
		};

		struct fmtData
		{
			unsigned short usFormat;
			unsigned short usChannels;
			unsigned long ulSampleRate;
			unsigned long ulByteRate;
			unsigned short usBlock;
			unsigned short usBits;
		};

		struct dataInfo
		{
			unsigned char strData[4];
			unsigned long ulLength;
		};

		struct chunkInfo
		{
			unsigned char strChunk[4];
			unsigned long ulChunkSize;
		};

		union unSample
		{
			short sIntData;
			unsigned char ucByteData[2];
		};


	public:
		ALwav();
		~ALwav();
		bool SLParseWAV2(const char*, wavInfo*, fmtInfo*, fmtData*, dataInfo*, chunkInfo*);
		bool SLwavstream(ALuint);
		bool CheckWAVHeader(char*, chunkInfo*);
		void SLplayWAV(const char* pathname, ALuint buffers[2], ALuint source);
		bool SLupdate();
		//bool SLReadPCMfile(const char *wavPath);
	};
	

}
