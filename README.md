# HaginGame (Unity 3D Project)

Unity 기반 3D 러너/생존 게임 프로젝트입니다.  
플레이어는 적 두 마리를 피해 달아나며, 체력과 스태미나를 관리하면서 살아남아야 합니다.

---

## 🎮 게임 특징
- **플레이어 조작**
  - 키보드 입력(WASD)으로 이동
  - **Dash 스킬** 구현 (빠른 회피 가능)
- **체력 기반 속도 변화**
  - HP가 줄어들수록 이동 속도 저하
- **적 추적 AI**
  - Enemy1, Enemy2는 플레이어를 추적하는 로직을 사용
- **게임 오버 조건**
  - 적과 충돌 → HP 감소
  - HP가 0이 되면 게임 종료

---

## 🛠️ 기술 스택
- **Unity** (C#)
- 상태 머신 기반 이동/대시/Idle 구현
- OOP 설계: `UnitBase` → `PlayerController`, `EnemyController`
- HP/스태미나/속도 등 **UnitStats 관리 시스템**

---

## 📂 프로젝트 구조 (일부)

Project_HaginGame/
┣ Assets/
┃ ┣ Scripts/
┃ ┃ ┣ Player/
┃ ┃ ┃ ┗ PlayerController.cs
┃ ┃ ┣ Enemy/
┃ ┃ ┃ ┗ EnemyController.cs
┃ ┃ ┣ Core/
┃ ┃ ┃ ┗ UnitBase.cs
┃ ┃ ┗ Managers/
┃ ┃ ┗ GameManager.cs
┃ ┣ Scenes/
┃ ┃ ┗ Main.unity
┗ ProjectSettings/


---

## 🚀 실행 방법
1. 저장소 클론
   ```bash
   git clone https://github.com/jinwoo33588/jinwoo33588-20242R0136COSE45700.git
2. Unity Hub → Project_HaginGame 폴더 열기

3. 메인 씬(Assets/Scenes/Main.unity) 실행


---

# 💼 포트폴리오용 요약 섹션


### Unity Game – HaginGame
**Unity3D 기반 러너/생존 게임**  
- 플레이어는 **적 두 마리의 추적 AI**를 피해 달아나야 함  
- **Dash 스킬**과 **체력 기반 속도 변화 시스템** 구현  
- **상태 머신 기반 캐릭터 제어** (Idle/Walk/Run/Dash)  
- OOP 구조: `UnitBase` 상속 → `PlayerController`, `EnemyController`  
- HP·스태미나·속도 등 **동적 스탯 관리 시스템** 설계  

👉 주요 성과: **기초적인 적 추적 AI**와 **플레이어 스킬 시스템**을 직접 설계/구현하여 Unity 프로젝트 구조화 및 확장 가능성을 고려한 설계 경험 확보

