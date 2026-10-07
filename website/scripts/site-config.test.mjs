import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';

test('site publishing and sitemap pointers target the MuiSim fork', async () => {
  const { default: config } = await import('../docusaurus.config.js');
  assert.equal(config.url, 'https://muisim.github.io');
  assert.equal(config.baseUrl, '/unity-mcp/');
  assert.equal(config.organizationName, 'MuiSim');
  assert.equal(config.projectName, 'unity-mcp');

  const robots = await readFile(new URL('../static/robots.txt', import.meta.url), 'utf8');
  assert.match(robots, /Sitemap: https:\/\/muisim\.github\.io\/unity-mcp\/sitemap\.xml/);
});

test('docs do not enable a telemetry beacon even when configured in the environment', async () => {
  const oldCode = process.env.GOATCOUNTER_CODE;
  process.env.GOATCOUNTER_CODE = 'test-remote-reporter';
  try {
    const { default: config } = await import('../docusaurus.config.js?telemetry-disabled');
    assert.equal(config.plugins.some((plugin) =>
      (Array.isArray(plugin) ? plugin[0] : plugin) === 'docusaurus-plugin-goatcounter'), false);
    assert.equal(config.themeConfig.goatcounter, undefined);
    assert.equal(JSON.stringify(config).includes('test-remote-reporter'), false);
  } finally {
    if (oldCode === undefined) delete process.env.GOATCOUNTER_CODE;
    else process.env.GOATCOUNTER_CODE = oldCode;
  }
});
