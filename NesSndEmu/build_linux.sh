g++ -fPIC -O2 -shared -I. -DLINUX DllWrapper.cpp Simple_Apu.cpp nes_apu/apu_snapshot.cpp nes_apu/Blip_Buffer.cpp nes_apu/Multi_Buffer.cpp nes_apu/Nes_Apu.cpp nes_apu/Nes_Namco.cpp nes_apu/Nes_Oscs.cpp nes_apu/Nes_Vrc6.cpp nes_apu/Nonlinear_Buffer.cpp -o NesSndEmu.so
