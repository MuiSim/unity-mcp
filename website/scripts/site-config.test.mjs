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

test('homepage and installation guide pin the stable fork release', async () => {
  const { default: config } = await import('../docusaurus.config.js');
  assert.equal(config.customFields.latestVersion, 'v10.3.0');
  const hero = await readFile(new URL('../src/components/HomeHero/index.js', import.meta.url), 'utf8');
  const install = await readFile(new URL('../docs/getting-started/install.md', import.meta.url), 'utf8');
  const unityUrl = 'https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#v10.3.0';
  assert.ok(hero.includes(unityUrl));
  assert.ok(install.includes(unityUrl));
  assert.ok(install.includes('https://github.com/MuiSim/unity-mcp/archive/v10.3.0.zip#subdirectory=Server'));
  assert.equal(hero.includes('UPM_BETA'), false);
  assert.equal(install.includes('https://github.com/MuiSim/unity-mcp.git?path=/MCPForUnity#main'), false);
  assert.ok(install.includes('aa5fc638d623f56178d50329d9ad8c541a57fe66'));
});
