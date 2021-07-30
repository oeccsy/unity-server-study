# Firebase 연동

유니티에 Firebase를 연동하였다.

## [Project Setting]

1. Firebase 웹사이트에서 프로젝트 생성
1. 생성한 Firebase 프로젝트에 유니티 앱 등록
	* google-service.json 다운로드 후 Asset 폴더에 추가
	* Firebase Unity SDK 다운로드 후 Import

1. Firebase가 앱을 식별할 수 있도록 앱의 인증키를 추가한다.  
    * 인증키 추가를 위해 유니티 프로젝트에서 keystore파일 생성  
    Build Settings -> Player Settings -> Player -> Publishing Settings -> Keystore Manager -> Keystore (create new)
    * 터미널에서 해당파일 위치로 이동한 후 생성한 명령어 입력  
    명령어 포맷 : `keytool -list -v -alias [alias 이름] -keystore [keystore 파일명]`  
    * 출력된 인증키를 아래의 경로에 입력  
    Firebase 웹사이트 -> 프로젝트 설정 -> 내 앱 -> 디지털 지문 추가
1. 인증키 추가 이후 google-service.json 업데이트