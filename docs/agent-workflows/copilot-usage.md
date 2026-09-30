# Copilot Usage for This Project

Verified against official VS Code and GitHub documentation on 2026-09-30. Copilot changes often; recheck before relying on details.

## Choosing a Surface

| Work | Surface | Why |
| --- | --- | --- |
| Architecture, OpenSpec refinement, cross-repo (CopyStart + mini-cluster + Tramites) | VS Code Agent, strongest reasoning model | Cloud agent works on one repository per session |
| Well-specified single-repo task with clear tests (e.g. backend skeleton, docs) | Copilot cloud agent from an issue | Runs in the background on a branch; 59-minute hard limit per session |
| Independent review of a finished change | Subagent or a read-only custom agent | Separates author and reviewer context |
| Mechanical edits | Agent with a faster, cheaper model | Save premium credits for reasoning-heavy work |

## Customization Rules

- Always-on project guidance: `AGENTS.md` (read by Copilot, cloud agent, Codex, and the Local agent).
- File-specific conventions: `.github/instructions/*.instructions.md` with `name`, `description`, and `applyTo`. Every instruction is path-scoped. Do not add a skill for link, version, or validation checks; run `python3 scripts/governance_checks.py`.
- Reusable workflows: skills in `.github/skills/<name>/SKILL.md`; `name` must match the folder, lowercase and hyphens only.
- Prompt files are deprecated for Agent Host sessions. Existing `opsx-*` prompts still work in the Local agent; prefer the equivalent `openspec-*` skills.
- Verify discovery in **Chat: Open Customizations** and check **References** in a response.

## Cloud Agent Tasks

- Create one issue per OpenSpec task group; link the change folder and state the validation commands.
- Cloud agent cannot touch mini-cluster in the same session; split infrastructure tasks by repository.
- Add a `copilot-setup-steps.yml` workflow with the .NET 10 SDK and Node before delegating build work.

## Sources

- https://code.visualstudio.com/docs/agent-customization/custom-instructions
- https://code.visualstudio.com/docs/agent-customization/agent-skills
- https://code.visualstudio.com/docs/agent-customization/custom-agents
- https://code.visualstudio.com/docs/agent-customization/overview
- https://docs.github.com/en/copilot/concepts/agents/coding-agent/about-coding-agent
