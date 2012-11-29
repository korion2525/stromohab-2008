// ManagedStaticClass.h

#pragma once

using namespace System;

namespace ALManagedStaticClass {

	public ref class ALsetup
	{
		static bool check();

	
		
	public:
		static bool SLsetenvironment();
		static void SLplayTone();
		static void SLchangepitch(float);
		static bool SLtonestream(unsigned int buffer);
		static bool SLtoneupdate();
		static bool SLtoneupdate(unsigned int freq);
		static void SLplayTone(unsigned int freq);
		static void SLvarypitch(float p);
		static void SLstoptone();
		static void SLcloseEnvironment();
		static bool SLreadWAV(unsigned int);
		static void SLPlayWAV();
		static void SLvarygain(float g);



		//static bool SLBufferWAV(unsigned int numberofbytes);


	};
}
