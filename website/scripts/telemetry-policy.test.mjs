import { test } from 'node:test';
import assert from 'node:assert/strict';

test('docs do not enable a telemetry beacon even when configured in the environment', async () => {
  const oldCode = process.env.GOATCOUNTER_CODE;
  process.env.GOATCOUNTER_CODE = 'test-remote-reporter';
  try {
    const { default: config } = await import('../docusaurus.config.js');
    assert.equal(config.plugins.some((plugin) =>
      (Array.isArray(plugin) ? plugin[0] : plugin) === 'docusaurus-plugin-goatcounter'), false);
    assert.equal(config.themeConfig.goatcounter, undefined);
    assert.equal(JSON.stringify(config).includes('test-remote-reporter'), false);
  } finally {
    if (oldCode === undefined) delete process.env.GOATCOUNTER_CODE;
    else process.env.GOATCOUNTER_CODE = oldCode;
  }
});
