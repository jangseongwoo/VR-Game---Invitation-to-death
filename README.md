

# Invitation to death - 4D VR Game
> 프로젝트 'ITD'입니다. [(주)모션디바이스](http://www.motion-device.com)와 산학협력으로 개발했으며 VR+Motion device가 있어야 정상적인 구동이 가능합니다. <br>
(전체 프로젝트 파일은 용량이 너무 커 올리지 않았습니다. 전체 프로젝트 파일을 원하시는 분은 하단 정보 카테고리에 있는 메일 주소로 메일을 보내주시기 바랍니다.) [![GitHub license](https://img.shields.io/github/license/Day8/re-frame.svg)](license.txt)


<img align="center" src="/images/Intro.png" width="900" height="300">


"죽은 여자친구를 되찾기 위해 지옥으로!"

탄광 열차를 타고 레일을 따라 움직이며 미션을 수행

약 1.5km의 레일 코스를 약 3분의 플레이 타임을 가지고 진행합니다. 


## 게임 소개

### 게임 플레이 화면

- 시작 후 1인칭, 3인칭 화면
<img align="left" src="/images/play1.png" width="410" height="250">
<img src="/images/play2.png" width="410" height="250">

- 전체 맵 
<p align="center">
<img src="./images/map1.png" alt="play2"
width="600" height="350">
</p>

- devil - monster modeling image

<img align="left" src="./images/devil.png" width="410" height="250">
<img src="./images/devil2.png" width="410" height="250">

- 게임 내 mission 최종보스 / 길막몹 / 갈림길 선택
<img align="left" src="./images/mission1.png" width="250" height="250">
<img src="./images/mission2.png" width="250" height="250">
<img src="./images/mission3.png" width="250" height="250">

- 실제 플레이 모습(VR과 Motion device 탑승 상태)
<img align="center" src="/images/play_1.png" width="450" height="250">

- 실제 플레이 영상(개발 50% 완료 플레이 영상, VR 미착용) <br>
[Link](https://youtu.be/gUbah-mU-zY?list=PL5hRdzUugwbRL76tTvyy7_AiprT0y24VE)


## 게임 방법
<img align="right" src="/images/Play_2.png" width="410" height="250">

- Oculus lift를 이용해 시야 조정을 합니다.
- 미사일 발사는 조이스틱의 A 버튼을 이용해 발사합니다.
- 핸들을 이용해 갈림길 미션에서 길을 선택할 수 있습니다.
<br>
<br>
<br>




## 설치 방법

OS X & 윈도우:

1. Git clone  - https://github.com/jangseongwoo/VR-Game---Invitation-to-death.git

2. Unity3d Project로 Invitation to death 폴더 열기

## 개발 목표 및 내용

- Unity3D의 물리엔진을 이용하여 현실감 넘치는 객차의 움직임을 구현

- ‘Oculus VR’, UDP 통신을 통한 ‘Motion Device’와의 연동을 통해 게임의 4자유도를 구현해 현실감을 증폭

- 3D-MAX, Z-brush를 이용한 3D모델링 제작

- ‘Motion Device’ 기업과의 연계를 통한 게임 개발 경험

## 기술 요소와 중점 연구 분야

- ‘오큘러스 리프트’의 헤드 트래킹 기술을 게임에 적용

- 레일 위를 움직이는 객차의 pitch, roll, yaw, heave, 속도 등의 값을 유니티 물리엔진과 스크립트를 통한 수학적 연산을 이용하여 얻어낸 후 UDP 통신을 통해 ‘모션 디바이스’에 전송하고 그에 따른 모션디바이스의 사실감 있는 움직임을 연구

- 3D Max, Zbrush를 이용한 모델링

- 상황에 적합한 사운드를 통한 사실감 증폭



## 사용 기술

- Unity3d, Visual Studio, C#, Oculus Lift, Motion device, 3D max, Photoshop, Zbush

## 게임의 장점

이 게임의 장점으로는 실감 나면서도 안전한 체험에 있습니다. <br>현실에선 불가능한 위험한 레일을 모션디바이스와 오큘러스 리프트의 장비효과를 살려 마음껏 즐길 수 있습니다.
여타 3D 체험관의 시뮬레이션들보다 사실감 넘치는 가상현실 체험과 상호작용이 가능합니다.

## 추가 개발 및 개선 요구사항

완성된 트랙이 하나 뿐 이기에 여러 번 게임을 즐기기에 부족한 부분이 있습니다. <br>이를 개선하기 위하여 추가적으로 트랙과 맵을 제작해야 할 필요가 있습니다.
Oculus DK2는 아직 미완성 제품으로 인지와 게임 화면 전환의 차이로 인한 멀미 현상이 있으며 모션디바이스 탑승으로 인한 멀미가 발생 할 수 있어, 장시간 게임을 즐기기에 무리가 따를 수 있습니다.

## 업데이트 내역

* 1.0.0
* version 1.0.0 작업 완료 (2015.10)
* 0.0.1
* 작업 진행 중

## 최소 시스템 요구 사항
- 시스템 운영체제: Windows XP 
- CPU 프로세서: Intel Core i3-4005U
- 램: 4GB  
- 그래픽카드: AMD Radeon 6630M
- 하드 드라이브 필요 용량: 1GB이상의 여유공간

## 정보

장성우 – [@facebook](https://www.facebook.com/profile.php?id=100007028118707&ref=bookmarks) – seongwoo.dev@gmail.com

MIT 라이센스를 준수하며 ``LICENSE``에서 자세한 정보를 확인할 수 있습니다.

[https://github.com/jangseongwoo/github-link](https://github.com/jangseongwoo/github-link)

<!-- Markdown link & img dfn's -->
[npm-image]: https://img.shields.io/npm/v/datadog-metrics.svg?style=flat-square
[npm-url]: https://npmjs.org/package/datadog-metrics
[npm-downloads]: https://img.shields.io/npm/dm/datadog-metrics.svg?style=flat-square
[travis-image]: https://img.shields.io/travis/dbader/node-datadog-metrics/master.svg?style=flat-square
[travis-url]: https://travis-ci.org/dbader/node-datadog-metrics
[wiki]: https://github.com/yourname/yourproject/wiki
