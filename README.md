# RUNSONIC
Sonic and Knuckles Collection modernized launcher

* Uses WPF to make the launcher readable on modern screens.
* Uses MVVM-Light to make the project manageable.
* Uses Costura.Fody (which requires a strong-named assembly) so the compiler outputs only a single EXE, and not a huge mess of DLLs along with it.
* Uses WPFLocalizationExtension-signed for localization. Old, but works, and it is strongly named.
* Uses PresentationFramework.Classic to mimic the original launcher's Windows 95-like style.
* Mimics all the behaviors of the old launcher (eg. keyboard support, single-instance limitation, localization, modal dialogs)

But WHY ?
----------
Because it was fun.
