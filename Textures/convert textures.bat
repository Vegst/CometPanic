@echo off
setlocal ENABLEDELAYEDEXPANSION

set /a found=0
set /a all=1
set inkscape="C:\Program Files\Inkscape\inkscape.exe"
set destination=D:\vegar\Documents\OneDrive - NTNU\Unity Projects\Comet Panic\Assets\Sprites

:Start
set /a files=0
for %%f in (*.svg) do (
	set /a files=files+1
)


echo .
echo Inkscape-path: %inkscape%
echo Destination-folder: %destination%
echo Edit to change paths
echo .
echo Found %files% .svg files to convert in current path
echo (1) Convert all files
echo (2) Convert missing files
set /p input=Enter number corresponding to your choice: 
if %input%==1	( set /a all=1 )
if %input%==2	( set /a all=0 )

echo .
echo How to convert?
echo (1) Convert to low density (120dpi) only
echo (2) Convert to medium density (160dpi) only
echo (3) Convert to high density (240dpi) only
echo (4) Convert to extra high density (320dpi) only
echo (5) Convert to extra-extra high density (480dpi) only
echo (6) Convert to all densities
set /p input=Enter number corresponding to your choice: 
if %input%==1	( GOTO ConvertLow )
if %input%==2	( GOTO ConvertMedium )
if %input%==3	( GOTO ConvertHigh )
if %input%==4	( GOTO ConvertExtraHigh )
if %input%==5	( GOTO ConvertExtraExtraHigh )
if %input%==6	( GOTO ConvertExtraExtraExtraHigh )
if %input%==7	( GOTO ConvertAll )
echo Error, couldn't handle your input

:ConvertLow
echo Converting all files now to low density...
if not exist "%destination%\drawable-ldpi" (
	md "%destination%\drawable-ldpi"
)
for %%f in (*.svg) do (
	if %all%==0 (
		if not exist "%destination%\drawable-ldpi\%%~nf.png" (
			echo Converting %%f...
			%inkscape% -f %%f -e "%destination%\drawable-ldpi\%%~nf.png" --export-dpi 120
		)
	)
	if %all%==1 (
		echo Converting %%f...
		%inkscape% -f %%f -e "%destination%\drawable-ldpi\%%~nf.png" --export-dpi 120
	)
)
echo Done!
GOTO Start

:ConvertMedium
echo Converting all files now to medium density...
if not exist "%destination%\drawable-mdpi" (
	md "%destination%\drawable-mdpi"
)
for %%f in (*.svg) do (
	if %all%==0 (
		if not exist "%destination%\drawable-mdpi\%%~nf.png" (
			echo Converting %%f...
			%inkscape% -f %%f -e "%destination%\drawable-mdpi\%%~nf.png" --export-dpi 160
		)
	)
	if %all%==1 (
		echo Converting %%f...
		%inkscape% -f %%f -e "%destination%\drawable-mdpi\%%~nf.png" --export-dpi 160
	)
)
echo Done!
GOTO Start

:ConvertHigh
echo Converting all files now to high density...
if not exist "%destination%\drawable-hdpi" (
	md "%destination%\drawable-hdpi"
)
for %%f in (*.svg) do (
	if %all%==0 (
		if not exist "%destination%\drawable-hdpi\%%~nf.png" (
			echo Converting %%f...
			%inkscape% -f %%f -e "%destination%\drawable-hdpi\%%~nf.png" --export-dpi 240
		)
	)
	if %all%==1 (
		echo Converting %%f...
		%inkscape% -f %%f -e "%destination%\drawable-hdpi\%%~nf.png" --export-dpi 240
	)
)
echo Done!
GOTO Start

:ConvertExtraHigh
echo Converting all files now to high density...
if not exist "%destination%\drawable-xhdpi" (
	md "%destination%\drawable-xhdpi"
)
for %%f in (*.svg) do (
	if %all%==0 (
		if not exist "%destination%\drawable-xhdpi\%%~nf.png" (
			echo Converting %%f...
			%inkscape% -f %%f -e "%destination%\drawable-xhdpi\%%~nf.png" --export-dpi 320
		)
	)
	if %all%==1 (
		echo Converting %%f...
		%inkscape% -f %%f -e "%destination%\drawable-xhdpi\%%~nf.png" --export-dpi 320
	)
)
echo Done!
GOTO Start

:ConvertExtraExtraHigh
echo Converting all files now to high density...
if not exist "%destination%\drawable-xxhdpi" (
	md "%destination%\drawable-xxhdpi"
)
for %%f in (*.svg) do (
	if %all%==0 (
		if not exist "%destination%\drawable-xxhdpi\%%~nf.png" (
			echo Converting %%f...
			%inkscape% -f %%f -e "%destination%\drawable-xxhdpi\%%~nf.png" --export-dpi 480
		)
	)
	if %all%==1 (
		echo Converting %%f...
		%inkscape% -f %%f -e "%destination%\drawable-xxhdpi\%%~nf.png" --export-dpi 480
	)
)
echo Done!
GOTO Start

:ConvertAll
echo Converting all files now to all densities(this may take some time)..
if not exist "%destination%\drawable-ldpi" (
	md "%destination%\drawable-ldpi"
)
if not exist "%destination%\drawable-mdpi" (
	md "%destination%\drawable-mdpi"
)
if not exist "%destination%\drawable-hdpi" (
	md "%destination%\drawable-hdpi"
)
if not exist "%destination%\drawable-xhdpi" (
	md "%destination%\drawable-xhdpi"
)
if not exist "%destination%\drawable-xxhdpi" (
	md "%destination%\drawable-xxhdpi"
)
for %%f in (*.svg) do (
	if %all%==0 (
		for %%d in (120, 160, 240, 320, 480) do (
			if %%d==120 (
				if not exist "%destination%\drawable-ldpi\%%~nf.png" (
					echo Converting %%f in low dpi...
					%inkscape% -f %%f -e "%destination%\drawable-ldpi\%%~nf.png" --export-dpi %%d
				)
			)
			if %%d==160 (
				if not exist "%destination%\drawable-mdpi\%%~nf.png" (
					echo Converting %%f in medium dpi...
					%inkscape% -f %%f -e "%destination%\drawable-mdpi\%%~nf.png" --export-dpi %%d
				)
			)
			if %%d==240 (
				if not exist "%destination%\drawable-hdpi\%%~nf.png" (
					echo Converting %%f in high dpi...
					%inkscape% -f %%f -e "%destination%\drawable-hdpi\%%~nf.png" --export-dpi %%d
				)
			)
			if %%d==320 (
				if not exist "%destination%\drawable-xhdpi\%%~nf.png" (
					echo Converting %%f in extra high dpi...
					%inkscape% -f %%f -e "%destination%\drawable-xhdpi\%%~nf.png" --export-dpi %%d
				)
			)
			if %%d==480 (
				if not exist "%destination%\drawable-xxhdpi\%%~nf.png" (
					echo Converting %%f in extra extra high dpi...
					%inkscape% -f %%f -e "%destination%\drawable-xxhdpi\%%~nf.png" --export-dpi %%d
				)
			)
		)
	)
	if %all%==1 (
		
		for %%d in (120, 160, 240, 320, 480) do (
			if %%d==120 (
				echo Converting %%f in low dpi...
				%inkscape% -f %%f -e "%destination%\drawable-ldpi\%%~nf.png" --export-dpi %%d
			)
			if %%d==160 (
				echo Converting %%f in medium dpi...
				%inkscape% -f %%f -e "%destination%\drawable-mdpi\%%~nf.png" --export-dpi %%d
			)
			if %%d==240 (
				echo Converting %%f in high dpi...
				%inkscape% -f %%f -e "%destination%\drawable-hdpi\%%~nf.png" --export-dpi %%d
			)
			if %%d==320 (
				echo Converting %%f in extra high dpi...
				%inkscape% -f %%f -e "%destination%\drawable-xhdpi\%%~nf.png" --export-dpi %%d
			)
			if %%d==480 (
				echo Converting %%f in extra extra high dpi...
				%inkscape% -f %%f -e "%destination%\drawable-xxhdpi\%%~nf.png" --export-dpi %%d
			)
		)
	)
)
echo Done!
GOTO Start

pause
