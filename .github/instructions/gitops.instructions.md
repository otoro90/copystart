---
name: 'GitOps and infrastructure'
description: 'Use when changing Dockerfiles, Kubernetes manifests, Kustomize overlays, CI workflows, or mini-cluster integration.'
applyTo: '{gitops/**,**/Dockerfile,.github/workflows/**}'
---
# GitOps Rules

- Images target linux/arm64 and are deployed by digest.
- This repository owns workload bases and overlays; mini-cluster owns Argo CD Applications and shared infrastructure.
- Cloudflare Tunnel routes only to Traefik. Tenant resolution depends on the preserved Host header.
- Secrets reach pods only through ESO from OpenBao. Never commit Secret values.
- Every infrastructure change declares its Day-1 stage, idempotency, readiness check, and rollback, per `mini-cluster/AGENTS.md`.
- Production overlays stay without automated sync until an approved cutover.
